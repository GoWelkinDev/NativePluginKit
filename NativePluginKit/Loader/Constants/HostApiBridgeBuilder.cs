using System.Runtime.InteropServices;

namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 用于构建 <see cref="HostApiTable"/> 的静态工厂类
    /// 将托管委托转换为非托管函数指针
    /// </summary>
    public static unsafe class HostApiBridgeBuilder
    {
        /// <summary>
        /// 表示场景切换函数的委托，与宿主API中的 <c>TransitionToScene</c> 匹配
        /// </summary>
        /// <param name="scenePathPtr">指向UTF-8字符串的指针，表示场景路径</param>
        /// <param name="fadeOut">是否使用淡出效果</param>
        public delegate void TransitionToSceneDelegate(IntPtr scenePathPtr, bool fadeOut);

        /// <summary>
        /// 表示日志输出函数的委托，与宿主API中的 <c>PrintLog</c> 匹配
        /// </summary>
        /// <param name="messagePtr">指向UTF-8字符串的指针，表示日志消息</param>
        public delegate void PrintLogDelegate(IntPtr messagePtr);

        /// <summary>
        /// 创建填充了函数指针的 <see cref="HostApiTable"/> 实例
        /// </summary>
        /// <param name="transition">实现场景切换的托管委托</param>
        /// <param name="printLog">实现日志输出的托管委托</param>
        /// <returns>包含非托管函数指针的API表结构体</returns>
        public static HostApiTable Create(TransitionToSceneDelegate transition, PrintLogDelegate printLog)
        {
            return new HostApiTable
            {
                TransitionToScene = (IntPtr)(delegate* unmanaged<IntPtr, bool, void>)Marshal.GetFunctionPointerForDelegate(transition),
                PrintLog = (IntPtr)(delegate* unmanaged<IntPtr, void>)Marshal.GetFunctionPointerForDelegate(printLog)
            };
        }
    }
}
