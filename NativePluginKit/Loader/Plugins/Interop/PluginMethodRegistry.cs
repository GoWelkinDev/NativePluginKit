// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Loader.Plugins.Interop
{
    /// <summary>
    /// 宿主侧维护的跨插件方法注册表，键为 (pluginName, methodName), 值为非托管函数指针
    /// </summary>
    public static class PluginMethodRegistry
    {
        private static readonly Dictionary<(string, string), IntPtr> _map = new();
        private static readonly ReaderWriterLockSlim _lock = new(LockRecursionPolicy.NoRecursion);

        public static bool Register(string plugin, string method, IntPtr fnPtr)
        {
            if (string.IsNullOrEmpty(plugin) || string.IsNullOrEmpty(method) || fnPtr == IntPtr.Zero)
                return false;

            _lock.EnterWriteLock();
            try
            {
                _map[(plugin, method)] = fnPtr;
                return true;
            }
            finally { _lock.ExitWriteLock(); }
        }

        public static bool Unregister(string plugin, string method)
        {
            _lock.EnterWriteLock();
            try { return _map.Remove((plugin, method)); }
            finally { _lock.ExitWriteLock(); }
        }

        public static IntPtr Get(string plugin, string method)
        {
            _lock.EnterReadLock();
            try
            {
                return _map.TryGetValue((plugin, method), out var p) ? p : IntPtr.Zero;
            }
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
                foreach (var key in _map.Keys.Where(k => k.Item1 == plugin).ToList())
                    _map.Remove(key);
            }
            finally { _lock.ExitWriteLock(); }
        }
    }
}
