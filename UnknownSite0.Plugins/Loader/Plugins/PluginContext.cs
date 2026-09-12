using NativePluginKit.Loader.Constants;

namespace NativePluginKit.Loader.Plugins
{
    /// <summary>
    /// 提供插件运行时的全局上下文，用于访问宿主 API 函数表
    /// </summary>
    public static class PluginContext
    {
        /// <summary>
        /// 获取当前插件使用的宿主 API 函数表
        /// 该属性在插件初始化时被设置
        /// </summary>
        public static HostApiTable CurrentApi { get; internal set; }
    }
}