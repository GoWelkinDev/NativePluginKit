// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Events.Handlers;
using System.Runtime.InteropServices;

namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 定义宿主向插件提供的 API 契约;
    /// 由 HostApiBridgeBuilder 转换为 HostApiTable
    /// </summary>
    /// <remarks>
    /// 默认实现的方法，宿主可以按需覆盖;
    /// 可选方法未被覆盖时，默认返回零值或执行空操作
    /// </remarks>
    public interface IHostApi
    {
        /// <summary>
        /// 释放由宿主分配的非托管 UTF-8 字符串
        /// </summary>
        void FreeString(IntPtr ptr) => Marshal.FreeCoTaskMem(ptr);

        /// <summary>
        /// 输出普通日志
        /// </summary>
        void PrintLog(IntPtr messagePtr) { }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        void PrintWarning(IntPtr messagePtr) { }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        void PrintError(IntPtr messagePtr) { }

        /// <summary>
        /// 输出调试日志
        /// </summary>
        void LogDebug(IntPtr messagePtr) { }

        /// <summary>
        /// 注册事件回调
        /// </summary>
        bool RegisterEvent(int eventType, IntPtr callback, IntPtr userData) => EventDispatcher.Register(eventType, callback, userData);

        /// <summary>
        /// 取消事件回调注册
        /// </summary>
        bool UnregisterEvent(int eventType, IntPtr callback, IntPtr userData) => EventDispatcher.Unregister(eventType, callback, userData);
    }
}
