// SPDX-License-Identifier: BSD-3-Clause
// Copyright (c) 2026, GoWelkin Network

using NativePluginKit.Loader.Constants;
using System.Collections.Concurrent;

namespace NativePluginKit.Loader.Plugins
{
    /// <summary>
    /// 所有插件的基类，提供与宿主程序交互的基础功能
    /// 插件继承此类并实现 <see cref="OnStart"/> 和 <see cref="OnStop"/> 方法
    /// </summary>
    public abstract unsafe class Plugin
    {
        private static HostApiTable _api;
        private static readonly ConcurrentDictionary<string, PluginRef> _refs = new();

        /// <summary>
        /// 由插件入口函数调用，用于初始化插件并保存宿主 API 函数表
        /// 通常在插件DLL的导出函数 <c>OnInit</c> 中调用
        /// </summary>
        /// <param name="hostApiTablePtr">指向 <see cref="HostApiTable"/> 结构体的非托管指针</param>
        /// <param name="pluginName">插件名称</param>
        /// <param name="instance">要初始化的插件实例</param>
        /// <remarks>
        /// 该方法将非托管结构体转换为托管对象，设置全局API，并触发 <see cref="OnStart"/>
        /// </remarks>
        protected static void Initialize(IntPtr hostApiTablePtr, string pluginName, Plugin instance)
        {
            _api = *(HostApiTable*)hostApiTablePtr.ToPointer();
            PluginContext.CurrentApi = _api;
            PluginContext.PluginName = pluginName;

            instance.OnStart();
        }

        /// <summary>
        /// 获取指定插件的引用句柄
        /// 同名插件返回同一实例以便方法指针缓存生效
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        public static PluginRef Get(string pluginName)
            => _refs.GetOrAdd(pluginName, static name => new PluginRef(name));

        /// <summary>
        /// 插件启动时调用的抽象方法，子类必须实现具体的初始化逻辑
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// 插件停止时调用的抽象方法，子类必须实现具体的清理逻辑
        /// </summary>
        public abstract void OnStop();
    }
}