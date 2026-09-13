namespace NativePluginKit.Events.Handlers
{
    /// <summary>
    /// 宿主侧的事件分发器。将游戏内事件路由到所有已注册的插件回调
    /// 必须在主线程调用 Raise
    /// </summary>
    public static class EventDispatcher
    {
        private readonly struct Subscription
        {
            public readonly IntPtr Callback;
            public readonly IntPtr UserData;
            public Subscription(IntPtr cb, IntPtr ud) { Callback = cb; UserData = ud; }
        }

        private static readonly Dictionary<EventType, List<Subscription>> _subs = new();
        private static readonly object _lock = new();

        /// <summary>
        /// 由插件通过 HostApi 调用
        /// </summary>
        public static bool Register(int eventType, IntPtr callback, IntPtr userData)
        {
            if (callback == IntPtr.Zero) return false;

            var type = (EventType)eventType;
            lock (_lock)
            {
                if (!_subs.TryGetValue(type, out var list))
                {
                    list = new List<Subscription>();
                    _subs[type] = list;
                }
                foreach (var s in list)
                    if (s.Callback == callback && s.UserData == userData)
                        return true;   // 已注册，幂等
                list.Add(new Subscription(callback, userData));
            }
            return true;
        }


        /// <summary>
        /// 由插件通过 HostApi 调用
        /// </summary>
        public static bool Unregister(int eventType, IntPtr callback)
        {
            var type = (EventType)eventType;
            lock (_lock)
            {
                if (!_subs.TryGetValue(type, out var list)) return false;
                for (int i = list.Count - 1; i >= 0; i--)
                    if (list[i].Callback == callback) list.RemoveAt(i);
                return true;
            }
        }

        /// <summary>
        /// 清空指定插件的所有订阅（插件卸载时调用）
        /// </summary>
        public static void ClearAll()
        {
            lock (_lock) _subs.Clear();
        }

        /// <summary>
        /// 触发事件,data 必须指向一个固定布局的结构体（含 EventHeader）
        /// 且仅在本次调用期间有效。插件如需保留数据必须自行复制
        /// </summary>
        public static unsafe void Raise(EventType type, IntPtr data)
        {
            List<Subscription> snapshot;
            lock (_lock)
            {
                if (!_subs.TryGetValue(type, out var list) || list.Count == 0) return;
                snapshot = new List<Subscription>(list);
            }

            foreach (var s in snapshot)
            {
                var fn = (delegate* unmanaged<int, IntPtr, IntPtr, void>)s.Callback;
                fn((int)type, data, s.UserData);
            }
        }

        /// <summary>
        /// 便捷重载：直接传结构体，内部取地址
        /// </summary>
        public static unsafe void Raise<T>(EventType type, ref T payload) where T : unmanaged
        {
            fixed (T* p = &payload)
            {
                Raise(type, (IntPtr)p);
            }
        }
    }
}