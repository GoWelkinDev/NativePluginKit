using System.Runtime.InteropServices;

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
        /// 指向 <c>TransitionToScene</c> 函数的指针
        /// 函数签名：<c>void TransitionToScene(IntPtr scenePathPtr, bool fadeOut)</c>
        /// </summary>
        public IntPtr TransitionToScene;

        /// <summary>
        /// 指向 <c>PrintLog</c> 函数的指针
        /// 函数签名：<c>void PrintLog(IntPtr messagePtr)</c>
        /// </summary>
        public IntPtr PrintLog;
    }

    /// <summary>
    /// 插件导出的初始化函数签名
    /// 插件必须导出一个名为 <c>OnInit</c> 的函数，参数为指向 <see cref="HostApiTable"/> 的指针
    /// </summary>
    /// <param name="hostApiTable">指向宿主API函数表的指针</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PluginInitDelegate(IntPtr hostApiTable);
}
