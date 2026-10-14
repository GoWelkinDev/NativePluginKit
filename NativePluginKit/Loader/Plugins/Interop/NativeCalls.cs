// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using System.Collections.Concurrent;

namespace NativePluginKit.Loader.Plugins.Interop
{
    /// <summary>
    /// 跨插件原生调用入口
    /// </summary>
    public static class NativeCalls
    {
        private static readonly ConcurrentDictionary<string, NativeCall> _calls = new();

        /// <summary>
        /// 获取指定插件的调用句柄
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        public static NativeCall Get(string pluginName)
            => _calls.GetOrAdd(pluginName, static name => new NativeCall(name));
    }
}
