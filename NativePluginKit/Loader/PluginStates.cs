// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Loader
{
    /// <summary>
    /// 插件生命周期状态
    /// </summary>
    public enum PluginStates
    {
        /// <summary>
        /// 插件已加载，正在读取元数据与版本检查
        /// </summary>
        Loading,

        /// <summary>
        /// 元数据就绪，正在调用 <c>OnInit</c>
        /// </summary>
        Initializing,

        /// <summary>
        /// <c>OnInit</c> 成功返回，插件正常运行
        /// </summary>
        Active,

        /// <summary>
        /// 正在调用 <c>OnStop</c>
        /// </summary>
        Stopping,

        /// <summary>
        /// <c>OnStop</c> 已返回或未定义，模块即将释放
        /// </summary>
        Stopped,

        /// <summary>
        /// 加载或初始化过程中失败
        /// </summary>
        Failed,
    }
}
