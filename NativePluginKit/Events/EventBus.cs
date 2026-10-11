using NativePluginKit.Events;
using NativePluginKit.Features.Logger;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace NativePluginKit.Features.Events
{
    /// <summary>
    /// 插件侧事件订阅入口，屏蔽函数指针与 Marshal 转换等细节，只暴露 <see cref="Action{T}"/> 委托
    /// </summary>
    /// <remarks>
    /// 内部按事件类型 T 分组，每种 T 使用一个独立的非托管分发器
    /// 由 NativeAOT 为每个封闭泛型类型生成独立实现
    /// </remarks>
    public static unsafe class EventBus
    {
        private const DynamicallyAccessedMemberTypes StructRequirements =
            DynamicallyAccessedMemberTypes.PublicConstructors |
            DynamicallyAccessedMemberTypes.NonPublicConstructors;

        /// <summary>
        /// 订阅指定类型的事件
        /// </summary>
        /// <typeparam name="T">事件数据结构体，必须是 unmanaged 类型</typeparam>
        /// <param name="type">事件类型</param>
        /// <param name="handler">事件回调，在主线程调用</param>
        public static void On<[DynamicallyAccessedMembers(StructRequirements)] T>(EventType type, Action<T> handler) where T : unmanaged
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
        /// 取消指定类型事件的订阅
        /// </summary>
        /// <typeparam name="T">事件数据结构体，必须是 unmanaged 类型</typeparam>
        /// <param name="type">事件类型</param>
        /// <param name="handler">要移除的事件回调</param>
        public static void Off<[DynamicallyAccessedMembers(StructRequirements)] T>(EventType type, Action<T> handler) where T : unmanaged
        {
            if (handler is null) return;

            lock (Registry<T>.Lock)
            {
                if (!Registry<T>.Slots.TryGetValue(type, out var slot)) return;

                slot.Handlers.Remove(handler);

                if (slot.Handlers.Count == 0 && slot.Registered)
                {
                    EventBridge.Unregister((int)type, DispatchPtr, GCHandle.ToIntPtr(slot.Handle));

                    if (slot.Handle.IsAllocated) slot.Handle.Free();
                    slot.Registered = false;
                }
            }
        }

        /// <summary>
        /// 由宿主通过函数指针调用的分发入口
        /// </summary>
        /// <param name="eventType">事件类型对应的数值</param>
        /// <param name="dataPtr">指向事件载荷的非托管指针</param>
        /// <param name="userData">注册时传入的 GCHandle 编码指针</param>
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

        private static IntPtr DispatchPtr
            => (IntPtr)(delegate* unmanaged<int, IntPtr, IntPtr, void>)&Dispatch;

        // 类型转换器
        private interface IEventConverter
        {
            void Invoke(IntPtr dataPtr);
        }

        private sealed class EventConverter<[DynamicallyAccessedMembers(StructRequirements)] T> : IEventConverter where T : unmanaged
        {
            private readonly List<Action<T>> _handlers;
            public EventConverter(List<Action<T>> handlers) { _handlers = handlers; }

            public void Invoke(IntPtr dataPtr)
            {
                var payload = *(T*)dataPtr.ToPointer();

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

        private static class Registry<[DynamicallyAccessedMembers(StructRequirements)] T> where T : unmanaged
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