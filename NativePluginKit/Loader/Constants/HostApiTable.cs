// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using System.Runtime.InteropServices;
using static NativePluginKit.Loader.Constants.HostApiBridgeBuilder;

#pragma warning disable CS1572
namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 主程序暴露给插件的函数指针表，包含插件可调用的宿主功能
    /// 结构体采用顺序布局，与非托管内存直接对应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HostApiTable
    {
        /// <summary>
        /// 释放由宿主分配的非托管 UTF-8 字符串
        /// </summary>
        /// <param name="ptr">要释放的非托管字符串指针</param>
        [HostApi("NativePluginKit.Features.Wrappers", "StringNative", "Free",
            typeof(FreeStringDelegate), typeof(FreeStringDelegate))]
        public IntPtr FreeString;

        /// <summary>
        /// 将当前插件导出的方法注册到宿主插件方法表供其他插件调用
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <param name="methodName">对外暴露的方法名</param>
        /// <param name="fnPtr">方法对应的非托管函数指针</param>
        /// <returns>注册成功返回 1, 否则返回 0</returns>
        [HostApi("NativePluginKit.Loader", "PluginRegistry", "RegisterMethodPointer",
            typeof(RegisterPluginMethodDelegate), typeof(RegisterPluginMethodPublicDelegate))]
        public IntPtr PluginMethods_Register;

        /// <summary>
        /// 从宿主插件方法表移除指定方法
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <param name="methodName">要移除的方法名</param>
        /// <returns>移除成功返回 1, 否则返回 0</returns>
        [HostApi("NativePluginKit.Loader", "PluginRegistry", "UnregisterMethodPointer",
            typeof(UnregisterPluginMethodDelegate), typeof(UnregisterPluginMethodPublicDelegate))]
        public IntPtr PluginMethods_Unregister;

        /// <summary>
        /// 查询其他插件已注册的方法指针
        /// </summary>
        /// <param name="pluginName">目标插件名称</param>
        /// <param name="methodName">目标方法名</param>
        /// <returns>方法对应的函数指针未找到时返回 <see cref="IntPtr.Zero"/></returns>
        [HostApi("NativePluginKit.Loader", "PluginRegistry", "GetMethodPointer",
            typeof(GetPluginMethodDelegate), typeof(GetPluginMethodPublicDelegate))]
        public IntPtr PluginMethods_Get;
    }

    /// <summary>
    /// 插件导出的初始化函数签名
    /// 插件必须导出一个名为 <c>OnInit</c> 的函数，参数为指向 <see cref="HostApiTable"/> 的指针
    /// </summary>
    /// <param name="hostApiTable">指向宿主 API 函数表的指针</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PluginInitDelegate(IntPtr hostApiTable);
}
