using System.Runtime.InteropServices;
using NativePluginKit.Loader.Constants;

namespace NativePluginKit.Loader.Plugins
{
    /// <summary>
    /// 所有插件的基类，提供与宿主程序交互的基础功能
    /// 插件继承此类并实现 <see cref="OnStart"/> 和 <see cref="OnStop"/> 方法
    /// </summary>
    public abstract class Plugin
    {
        private static HostApiTable _api;
        private static IntPtr _pluginInfoPtr;

        /// <summary>
        /// 获取宿主程序提供的 API 函数表，插件可通过它调用宿主功能
        /// </summary>
        public HostApiTable Api => _api;

        /// <summary>
        /// 插件名称
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 插件描述信息
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// 插件作者
        /// </summary>
        public abstract string Author { get; }

        /// <summary>
        /// 插件版本号，默认为所在程序集的版本号
        /// </summary>
        public virtual Version? Version => GetType().Assembly.GetName().Version;

        /// <summary>
        /// 插件所要求的宿主程序API版本
        /// </summary>
        public abstract Version RequiredApiVersion { get; }

        /// <summary>
        /// 在插件静态构造函数中调用，注册插件元数据
        /// 从传入实例读取属性并分配非托管内存保存 <see cref="PluginInfo"/>
        /// 该方法确保 <see cref="GetPluginInfo"/> 在 <c>OnInit</c> 之前即可返回有效指针
        /// </summary>
        /// <param name="plugin">用于读取元数据的插件实例</param>
        protected static void SetPluginInfo(Plugin plugin)
        {
            var info = new PluginInfo
            {
                NamePtr = Marshal.StringToCoTaskMemUTF8(plugin.Name),
                DescriptionPtr = Marshal.StringToCoTaskMemUTF8(plugin.Description),
                AuthorPtr = Marshal.StringToCoTaskMemUTF8(plugin.Author),
                MajorVersion = plugin.Version?.Major ?? 0,
                MinorVersion = plugin.Version?.Minor ?? 0,
                BuildVersion = plugin.Version?.Build ?? 0,
                RevisionVersion = plugin.Version?.Revision ?? 0,
                RequiredApiMajor = plugin.RequiredApiVersion.Major,
                RequiredApiMinor = plugin.RequiredApiVersion.Minor
            };

            _pluginInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf<PluginInfo>());
            Marshal.StructureToPtr(info, _pluginInfoPtr, false);
        }

        /// <summary>
        /// 返回插件信息结构体指针，供宿主程序查询
        /// </summary>
        [UnmanagedCallersOnly(EntryPoint = "GetPluginInfo")]
        public static IntPtr GetPluginInfo()
        {
            return _pluginInfoPtr;
        }

        /// <summary>
        /// 由插件入口函数调用，用于初始化插件并保存宿主 API 函数表
        /// 通常在插件DLL的导出函数 <c>OnInit</c> 中调用
        /// </summary>
        /// <param name="hostApiTablePtr">指向 <see cref="HostApiTable"/> 结构体的非托管指针</param>
        /// <param name="instance">要初始化的插件实例</param>
        /// <remarks>
        /// 该方法将非托管结构体转换为托管对象，设置全局API，并触发 <see cref="OnStart"/>
        /// </remarks>
        protected static void Initialize(IntPtr hostApiTablePtr, Plugin instance)
        {
            _api = Marshal.PtrToStructure<HostApiTable>(hostApiTablePtr);
            PluginContext.CurrentApi = _api;
            PluginContext.PluginName = instance.Name;

            instance.OnStart();
        }

        /// <summary>
        /// 插件启动时调用的抽象方法，子类必须实现具体的初始化逻辑
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// 插件停止时调用的抽象方法，子类必须实现具体的清理逻辑
        /// </summary>
        public abstract void OnStop();
    }
}