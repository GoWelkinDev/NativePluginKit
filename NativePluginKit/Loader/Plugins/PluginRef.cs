// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Features.Plugins;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace NativePluginKit.Loader.Plugins
{
    /// <summary>
    /// 跨插件调用的引用句柄
    /// 持有目标插件名称与已解析的方法指针缓存
    /// </summary>
    public sealed class PluginRef
    {
        private readonly ConcurrentDictionary<string, IntPtr> _methodCache = new();

        /// <summary>
        /// 目标插件名称
        /// </summary>
        public string PluginName { get; }

        internal PluginRef(string pluginName) => PluginName = pluginName;

        /// <summary>
        /// 检查目标插件是否已注册指定方法
        /// </summary>
        /// <param name="method">方法名称</param>
        public bool HasMethod(string method) => PluginMethods.Get(PluginName, method) != IntPtr.Zero;

        /// <summary>
        /// 解析方法指针首次跨 ABI 查询后续从本地缓存读取
        /// </summary>
        /// <param name="method">方法名称</param>
        /// <exception cref="InvalidOperationException">方法未注册</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IntPtr Resolve(string method)
        {
            if (_methodCache.TryGetValue(method, out var ptr))
                return ptr;

            ptr = PluginMethods.Get(PluginName, method);
            if (ptr == IntPtr.Zero)
                throw new InvalidOperationException(
                    $"Plugin method '{PluginName}.{method}' is not registered.");

            _methodCache[method] = ptr;
            return ptr;
        }
    }
}
