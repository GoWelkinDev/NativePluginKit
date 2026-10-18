// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Loader.Plugins.Metadata;

namespace NativePluginKit.Loader
{
    /// <summary>
    /// 宿主侧插件注册表，承载插件生命周期记录与导出方法表
    /// </summary>
    public static partial class PluginRegistry
    {
        private const string SyntheticPrefix = "__";

        private static readonly Dictionary<string, PluginRecord> _plugins = new();
        private static readonly ReaderWriterLockSlim _lock = new(LockRecursionPolicy.NoRecursion);

        /// <summary>
        /// 尝试创建一条插件记录
        /// </summary>
        /// <returns>名称已被占用时返回 <c>null</c></returns>
        internal static PluginRecord? CreateRecord(string name, string dllPath, PluginInfo info, IntPtr moduleHandle)
        {
            _lock.EnterWriteLock();
            try
            {
                if (_plugins.ContainsKey(name)) return null;

                var plugin = new PluginRecord(name, dllPath, info, moduleHandle);
                _plugins[name] = plugin;
                return plugin;
            }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 更新插件状态
        /// </summary>
        /// <returns>记录不存在时返回 <c>null</c></returns>
        internal static bool TrySetState(string name, PluginStates state)
        {
            _lock.EnterWriteLock();
            try
            {
                if (!_plugins.TryGetValue(name, out var plugin)) return false;
                if (plugin.IsSynthetic) return false;
                plugin.State = state;
                return true;
            }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 将插件标记为加载失败状态，并清空其模块句柄
        /// </summary>
        /// <param name="name">插件名称</param>
        /// <returns></returns>
        internal static bool TryMarkFailed(string name)
        {
            _lock.EnterWriteLock();
            try
            {
                if (!_plugins.TryGetValue(name, out var plugin)) return false;
                plugin.State = PluginStates.Failed;
                plugin.ModuleHandle = IntPtr.Zero;
                return true;
            }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 获取指定插件的记录
        /// </summary>
        /// <returns>记录不存在时返回 <c>null</c></returns>
        internal static PluginRecord? GetRecord(string name)
        {
            _lock.EnterReadLock();
            try { return _plugins.TryGetValue(name, out var p) ? p : null; }
            finally { _lock.ExitReadLock(); }
        }

        /// <summary>
        /// 枚举所有插件记录，含 <see cref="PluginStates.Failed"/> 状态的记录
        /// </summary>
        internal static PluginRecord[] GetAllRecords()
        {
            _lock.EnterReadLock();
            try { return _plugins.Values.ToArray(); }
            finally { _lock.ExitReadLock(); }
        }

        /// <summary>
        /// 注册插件导出方法
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <param name="method">方法名称</param>
        /// <param name="fnPtr">函数指针</param>
        /// <returns></returns>
        internal static bool Register(string pluginName, string method, IntPtr fnPtr)
        {
            if (string.IsNullOrEmpty(pluginName) || string.IsNullOrEmpty(method) || fnPtr == IntPtr.Zero)
                return false;

            _lock.EnterWriteLock();
            try
            {
                if (!_plugins.TryGetValue(pluginName, out var plugin))
                {
                    if (!pluginName.StartsWith(SyntheticPrefix, StringComparison.Ordinal))
                        return false;

                    plugin = PluginRecord.CreateSynthetic(pluginName);
                    _plugins[pluginName] = plugin;
                }

                plugin.Methods[method] = fnPtr;
                return true;
            }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 注销插件导出方法
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <param name="method">方法名称</param>
        /// <returns></returns>
        internal static bool Unregister(string pluginName, string method)
        {
            _lock.EnterWriteLock();
            try 
            {
                return _plugins.TryGetValue(pluginName, out var plugin)
                    && plugin.Methods.Remove(method);
            }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 获取插件导出方法的函数指针
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <param name="method">方法名称</param>
        /// <returns></returns>
        internal static IntPtr Get(string pluginName, string method)
        {
            _lock.EnterReadLock();
            try
            {
                return _plugins.TryGetValue(pluginName, out var plugin)
                    && plugin.Methods.TryGetValue(method, out var ptr) ? ptr : IntPtr.Zero;
            }
            finally { _lock.ExitReadLock(); }
        }

        /// <summary>
        /// 检查指定插件是否已注册
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <returns></returns>
        public static bool HasPlugin(string pluginName)
        {
            _lock.EnterReadLock();
            try { return _plugins.ContainsKey(pluginName); }
            finally { _lock.ExitReadLock(); }
        }

        /// <summary>
        /// 插件卸载时清理其记录
        /// </summary>
        public static void ClearPlugin(string pluginName)
        {
            _lock.EnterWriteLock();
            try
            {
                _plugins.Remove(pluginName);
            }
            finally { _lock.ExitWriteLock(); }
        }

        /// <summary>
        /// 插件运行时记录
        /// </summary>
        internal sealed class PluginRecord
        {
            /// <summary>
            /// 插件名，来自 <c>PluginInfo.Name</c>
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// 插件在文件系统上的完整路径
            /// </summary>
            public string DllPath { get; }

            /// <summary>
            /// 当前生命周期状态
            /// </summary>
            public PluginStates State { get; internal set; }

            /// <summary>
            /// 是否为宿主侧导出方法占位插件
            /// </summary>
            public bool IsSynthetic { get; }

            /// <summary>
            /// 模块句柄; <see cref="PluginStates.Failed"/> 状态下为 <see cref="IntPtr.Zero"/>
            /// </summary>
            public IntPtr ModuleHandle { get; internal set; }

            /// <summary>
            /// 插件元数据快照
            /// </summary>
            public PluginInfo Info { get; internal set; }

            /// <summary>
            /// 方法名到非托管函数指针的映射
            /// </summary>
            internal Dictionary<string, IntPtr> Methods { get; } = new();

            internal PluginRecord(string name, string dllPath, PluginInfo info, IntPtr moduleHandle)
            {
                Name = name;
                DllPath = dllPath;
                Info = info;
                ModuleHandle = moduleHandle;
                State = PluginStates.Loading;
            }

            private PluginRecord(string syntheticName)
            {
                Name = syntheticName;
                DllPath = string.Empty;
                Info = default;
                ModuleHandle = IntPtr.Zero;
                State = PluginStates.Active;
                IsSynthetic = true;
            }

            internal static PluginRecord CreateSynthetic(string name) => new(name);
        }
    }
}
