// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Events.Handlers;
using System.Runtime.InteropServices;

namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 用于构建 <see cref="HostApiTable"/> 的静态工厂类
    /// 将托管委托转换为非托管函数指针
    /// </summary>
    public static class HostApiBridgeBuilder
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FreeStringDelegate(IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int RegisterPluginMethodDelegate(IntPtr pluginName, IntPtr methodName, IntPtr fnPtr);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int RegisterPluginMethodPublicDelegate(string pluginName, string methodName, IntPtr fnPtr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int UnregisterPluginMethodDelegate(IntPtr pluginName, IntPtr methodName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int UnregisterPluginMethodPublicDelegate(string pluginName, string methodName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetPluginMethodDelegate(IntPtr pluginName, IntPtr methodName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetPluginMethodPublicDelegate(string pluginName, string methodName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PrintLogDelegate(IntPtr messagePtr);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PrintLogPublicDelegate(string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RegisterEventDelegate(int eventType, IntPtr callback, IntPtr userData);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool UnregisterEventDelegate(int eventType, IntPtr callback, IntPtr userData);

        private static readonly List<Delegate> _keepAlive = new();

        private static IntPtr Ptr<TDelegate>(TDelegate d) where TDelegate : Delegate
        {
            _keepAlive.Add(d);
            return Marshal.GetFunctionPointerForDelegate(d);
        }

        /// <summary>
        /// 从宿主实现的 <see cref="IHostApi"/> 构建 <see cref="HostApiTable"/>
        /// </summary>
        /// <param name="api">宿主实现的接口实例</param>
        /// <returns>包含非托管函数指针的 API 表结构体</returns>
        public static HostApiTable Create(IHostApi api)
        {
            return new HostApiTable
            {
                FreeString = Ptr<FreeStringDelegate>(api.FreeString),
                PluginMethods_Register = Ptr<RegisterPluginMethodDelegate>(static (namePtr, methodPtr, fnPtr) =>
                    PluginRegistry.Register(
                        Marshal.PtrToStringUTF8(namePtr) ?? "",
                        Marshal.PtrToStringUTF8(methodPtr) ?? "",
                        fnPtr) ? 1 : 0),

                PluginMethods_Unregister = Ptr<UnregisterPluginMethodDelegate>(static (namePtr, methodPtr) =>
                    PluginRegistry.Unregister(
                        Marshal.PtrToStringUTF8(namePtr) ?? "",
                        Marshal.PtrToStringUTF8(methodPtr) ?? "") ? 1 : 0),

                PluginMethods_Get = Ptr<GetPluginMethodDelegate>(static (namePtr, methodPtr) =>
                    PluginRegistry.Get(
                        Marshal.PtrToStringUTF8(namePtr) ?? "",
                        Marshal.PtrToStringUTF8(methodPtr) ?? "")),

                PrintLog = Ptr<PrintLogDelegate>(api.PrintLog),
                PrintWarning = Ptr<PrintLogDelegate>(api.PrintWarning),
                PrintError = Ptr<PrintLogDelegate>(api.PrintError),
                LogDebug = Ptr<PrintLogDelegate>(api.LogDebug),

                RegisterEventCallback = Ptr<RegisterEventDelegate>(static (eventType, callback, userData) =>
                    EventDispatcher.Register(eventType, callback, userData)),
                UnregisterEventCallback = Ptr<UnregisterEventDelegate>(static (eventType, callback, userData) =>
                    EventDispatcher.Unregister(eventType, callback, userData)),
            };
        }
    }
}
