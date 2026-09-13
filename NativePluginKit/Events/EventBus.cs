using NativePluginKit.Events;
using NativePluginKit.Features.Logger;
using System.Runtime.InteropServices;

namespace NativePluginKit.Features.Events
{
    /// <summary>
    /// 插件侧事件订阅入口
    /// 屏蔽函数指针、Marshal 转换等细节，只暴露 <see cref="Action{T}"/> 委托
    /// </summary>
    /// <remarks>
    /// 内部按事件类型 T 分组，每种 T 使用一个独立的非托管分发器
    /// 由 NativeAOT 为每个封闭泛型类型生成独立实现
    /// </remarks>
    public static class EventBus
    {
        /// <summary>订阅指定类型的事件</summary>
        /// <typeparam name="T">事件数据结构体（unmanaged）</typeparam>
        /// <param name="type">事件类型</param>
        /// <param name="handler">事件回调，在主线程调用</param>
        public static void On<T>(EventType type, Action<T> handler) where T : unmanaged
        {
            if (handler is null) return;

            lock (Registry<T>.Lock)
            {
                if (!Registry<T>.Slots.TryGetValue(type, out var slot))
                {
                    slot = new Registry<T>.Slot();
                    Registry<T>.Slots[type] = slot;
                }

                if (!slot.Handlers.Contains(handler))
                    slot.Handlers.Add(handler);

                if (!slot.Registered)
                {
                    var converter = new EventConverter<T>(slot.Handlers);
                    slot.Handle = GCHandle.Alloc(converter, GCHandleType.Normal);
                    slot.Registered = true;

                    EventBridge.Register((int)type, DispatchPtr, GCHandle.ToIntPtr(slot.Handle));
                }
            }
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        public static void Off<T>(EventType type, Action<T> handler) where T : unmanaged
        {
            if (handler is null) return;

            lock (Registry<T>.Lock)
            {
                if (!Registry<T>.Slots.TryGetValue(type, out var slot)) return;

                slot.Handlers.Remove(handler);

                if (slot.Handlers.Count == 0 && slot.Registered)
                {
                    EventBridge.Unregister((int)type, DispatchPtr);
                    if (slot.Handle.IsAllocated) slot.Handle.Free();
                    slot.Registered = false;
                }
            }
        }

        /// <summary>
        /// 由宿主通过函数指针调用
        /// </summary>
        [UnmanagedCallersOnly]
        private static void Dispatch(int eventType, IntPtr dataPtr, IntPtr userData)
        {
            if (dataPtr == IntPtr.Zero || userData == IntPtr.Zero) return;

            var handle = GCHandle.FromIntPtr(userData);
            if (handle.Target is not IEventConverter converter) return;

            try
            {
                converter.Invoke(dataPtr);
            }
            catch (Exception ex)
            {
                try { Log.PrintError($"[EventBus] handler failed: {ex}"); }
                catch { /* 日志失败时静默 */ }
            }
        }

        private static unsafe IntPtr DispatchPtr
            => (IntPtr)(delegate* unmanaged<int, IntPtr, IntPtr, void>)&Dispatch;

        // 类型转换器
        private interface IEventConverter
        {
            void Invoke(IntPtr dataPtr);
        }

        private sealed class EventConverter<T> : IEventConverter where T : unmanaged
        {
            private readonly List<Action<T>> _handlers;
            public EventConverter(List<Action<T>> handlers) { _handlers = handlers; }

            public void Invoke(IntPtr dataPtr)
            {
                var payload = Marshal.PtrToStructure<T>(dataPtr);

                Action<T>[] snapshot;
                lock (_handlers) snapshot = _handlers.ToArray();

                foreach (var h in snapshot)
                {
                    try { h(payload); }
                    catch (Exception ex)
                    {
                        try { Log.PrintError($"[EventBus] handler threw: {ex}"); }
                        catch { }
                    }
                }
            }
        }

        // ==================================================================
        // 按 T 分组的注册表
        // ==================================================================

        private static class Registry<T> where T : unmanaged
        {
            public static readonly Dictionary<EventType, Slot> Slots = new();
            public static readonly object Lock = new();

            public sealed class Slot
            {
                public readonly List<Action<T>> Handlers = new();
                public GCHandle Handle;
                public bool Registered;
            }
        }
    }
}