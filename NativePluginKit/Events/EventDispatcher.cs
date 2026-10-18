// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Events
{
    /// <summary>
    /// 宿主侧的事件分发器，将游戏内事件路由到所有已注册的插件回调
    /// </summary>
    public static class EventDispatcher
    {
        private readonly struct Subscription
        {
            public readonly IntPtr Callback;
            public readonly IntPtr UserData;
            public Subscription(IntPtr cb, IntPtr ud) { Callback = cb; UserData = ud; }
        }

        private static readonly Dictionary<int, List<Subscription>> _subs = new();
        private static readonly object _lock = new();

        /// <summary>
        /// 注册事件回调
        /// </summary>
        /// <param name="eventType">事件类型对应的数值</param>
        /// <param name="callback">回调函数指针</param>
        /// <param name="userData">用户自定义数据</param>
        /// <returns>注册成功返回 true</returns>
        public static bool Register(int eventType, IntPtr callback, IntPtr userData)
        {
            if (callback == IntPtr.Zero) return false;

            lock (_lock)
            {
                if (!_subs.TryGetValue(eventType, out var list))
                {
                    list = new List<Subscription>();
                    _subs[eventType] = list;
                }
                foreach (var s in list)
                    if (s.Callback == callback && s.UserData == userData)
                        return true;   // 已注册，幂等
                list.Add(new Subscription(callback, userData));
            }
            return true;
        }


        /// <summary>
        /// 注销事件回调
        /// </summary>
        /// <param name="eventType">事件类型对应的数值</param>
        /// <param name="callback">回调函数指针</param>
        /// <param name="userData">注册时传入的用户自定义数据</param>
        /// <returns>注销成功返回 true</returns>
        public static bool Unregister(int eventType, IntPtr callback, IntPtr userData)
        {
            if (callback == IntPtr.Zero) return false;

            lock (_lock)
            {
                if (!_subs.TryGetValue(eventType, out var list)) return false;

                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].Callback == callback && list[i].UserData == userData)
                    {
                        list.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 清空指定插件的所有事件订阅
        /// </summary>
        public static void ClearAll()
        {
            lock (_lock) _subs.Clear();
        }

        /// <summary>
        /// 触发指定类型的事件, 必须在主线程调用, data 必须指向一个含 <see cref="EventHeader"/> 的固定布局结构体
        /// <para>该指针仅在本次调用期间有效，插件如需保留数据必须自行复制</para>
        /// </summary>
        /// <param name="eventType">要触发的事件类型</param>
        /// <param name="data">指向事件载荷的非托管指针</param>
        public static unsafe void Raise(int eventType, IntPtr data)
        {
            List<Subscription> snapshot;
            lock (_lock)
            {
                if (!_subs.TryGetValue(eventType, out var list) || list.Count == 0) return;
                snapshot = new List<Subscription>(list);
            }

            foreach (var s in snapshot)
            {
                var fn = (delegate* unmanaged<int, IntPtr, IntPtr, void>)s.Callback;
                fn(eventType, data, s.UserData);
            }
        }

        public static unsafe void Raise<T>(int eventType, ref T payload) where T : unmanaged
        {
            fixed (T* p = &payload)
            {
                Raise(eventType, (IntPtr)p);
            }
        }
    }
}