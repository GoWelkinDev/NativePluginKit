// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Loader.Plugins.NativeInterop;

namespace NativePluginKit.Features.Configs
{
    /// <summary>
    /// 宿主配置与运行时信息查询
    /// </summary>
    public static class Config
    {
        private static readonly NativeCall Host = NativeCalls.Get("__host__:Configs");
        
        /// <summary>
        /// 返回宿主指定的插件目录路径
        /// </summary>
        public static string GetPluginsDirectory()
        {
            return Host.Invoke<string>(nameof(GetPluginsDirectory));
        }

        /// <summary>
        /// 返回自宿主启动以来经过的秒数
        /// </summary>
        public static double GetTimeSinceStartup()
            => Host.Invoke<double>(nameof(GetTimeSinceStartup));
    }
}
