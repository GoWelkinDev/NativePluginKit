// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Loader.Plugins.Interop
{
    /// <summary>
    /// 单个插件在宿主侧的方法注册槽位
    /// </summary>
    public sealed class PluginSlot
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public string PluginName { get; }

        /// <summary>
        /// 方法名到非托管函数指针的映射
        /// </summary>
        internal Dictionary<string, IntPtr> Methods { get; } = new();

        internal PluginSlot(string pluginName) => PluginName = pluginName;
    }
}
