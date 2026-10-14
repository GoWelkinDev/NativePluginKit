// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Loader.Plugins.Interop
{
    /// <summary>
    /// 宿主侧维护的跨插件方法注册表
    /// </summary>
    public static class PluginMethodRegistry
    {
        private static readonly Dictionary<string, PluginSlot> _slots = new();
        private static readonly ReaderWriterLockSlim _lock = new(LockRecursionPolicy.NoRecursion);

        public static bool Register(string plugin, string method, IntPtr fnPtr)
        {
            if (string.IsNullOrEmpty(plugin) || string.IsNullOrEmpty(method) || fnPtr == IntPtr.Zero)
                return false;

            _lock.EnterWriteLock();
            try
            {
                if(!_slots.TryGetValue(plugin, out var slot))
                {
                    slot = new PluginSlot();
                    _slots[plugin] = slot;
                }
                slot.Methods[method] = fnPtr;
                return true;
            }
            finally { _lock.ExitWriteLock(); }
        }

        public static bool Unregister(string plugin, string method)
        {
            _lock.EnterWriteLock();
            try 
            {
                return _slots.TryGetValue(plugin, out var slot)
                    && slot.Methods.Remove(method);
            }
            finally { _lock.ExitWriteLock(); }
        }

        public static IntPtr Get(string plugin, string method)
        {
            _lock.EnterReadLock();
            try
            {
                return _slots.TryGetValue(plugin, out var slot)
                    && slot.Methods.TryGetValue(method, out var ptr) ? ptr : IntPtr.Zero;
            }
            finally { _lock.ExitReadLock(); }
        }

        public static bool HasPlugin(string plugin)
        {
            _lock.EnterReadLock();
            try { return _slots.ContainsKey(plugin); }
            finally { _lock.ExitReadLock(); }
        }

        /// <summary>
        /// 插件卸载时清理其全部注册，避免悬空指针
        /// </summary>
        public static void ClearPlugin(string plugin)
        {
            _lock.EnterWriteLock();
            try
            {
                _slots.Remove(plugin);
            }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 单个插件在宿主侧的方法注册槽位
        /// </summary>
        private sealed class PluginSlot
        {
            /// <summary>
            /// 方法名到非托管函数指针的映射
            /// </summary>
            internal Dictionary<string, IntPtr> Methods { get; } = new();
        }
    }
}
