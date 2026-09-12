using System.Runtime.InteropServices;
using NativePluginKit.Loader.Plugins;

namespace NativePluginKit.Features.Logger
{
    /// <summary>
    /// 提供插件端日志输出功能，调用宿主API的 <c>PrintLog</c> 函数
    /// </summary>
    public unsafe class Log
    {
        /// <summary>
        /// 向宿主程序输出一条日志消息
        /// </summary>
        /// <param name="message">要输出的日志内容</param>
        /// <remarks>
        /// 消息被转换为非托管UTF-8字符串后传递给宿主API，调用后立即释放内存
        /// </remarks>
        public static void PrintLog(string message)
        {
            var api = PluginContext.CurrentApi;
            var printFunc = (delegate* unmanaged<IntPtr, void>)api.PrintLog;
            IntPtr msgPtr = Marshal.StringToCoTaskMemUTF8(message);
            printFunc(msgPtr);
            Marshal.FreeCoTaskMem(msgPtr);
        }
    }
}
