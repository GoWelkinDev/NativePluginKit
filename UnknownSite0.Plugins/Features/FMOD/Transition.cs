using System.Runtime.InteropServices;
using NativePluginKit.Loader.Plugins;

namespace NativePluginKit.Features.FMOD
{
    /// <summary>
    /// 提供场景切换功能，调用宿主API的 <c>TransitionToScene</c> 函数
    /// </summary>
    public unsafe class Transition
    {
        /// <summary>
        /// 请求宿主程序切换到指定场景
        /// </summary>
        /// <param name="scenePath">场景路径 (UTF-8)</param>
        /// <param name="fadeOut">是否使用淡出过渡效果，默认为 <c>true</c></param>
        /// <remarks>
        /// 场景路径以非托管UTF-8字符串传递，调用后立即释放内存
        /// </remarks>
        public static void TransitionToScene(string scenePath, bool fadeOut = true)
        {
            var api = PluginContext.CurrentApi;
            var transitionFunc = (delegate* unmanaged<IntPtr, bool, void>)api.TransitionToScene;
            IntPtr pathPtr = Marshal.StringToCoTaskMemUTF8(scenePath);
            transitionFunc(pathPtr, fadeOut);
            Marshal.FreeCoTaskMem(pathPtr);
        }
    }
}
