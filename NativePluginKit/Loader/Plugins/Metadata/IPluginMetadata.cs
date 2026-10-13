// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Loader.Plugins.Metadata
{
    /// <summary>
    /// 插件元数据接口
    /// </summary>
    public interface IPluginMetadata
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        static abstract string Name { get; }

        /// <summary>
        /// 插件描述信息
        /// </summary>
        static abstract string Description { get; }

        /// <summary>
        /// 插件作者
        /// </summary>
        static abstract string Author { get; }

        /// <summary>
        /// 插件版本号
        /// </summary>
        static abstract Version Version { get; }

        /// <summary>
        /// 插件所要求的宿主程序API版本
        /// </summary>
        static abstract Version RequiredApiVersion { get; }
    }
}
