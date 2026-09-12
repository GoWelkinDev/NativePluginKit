using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnknownSite0.Plugins.Features.Logger;
using UnknownSite0.Plugins.Loader.Plugins;

namespace HelloWorldPlugin
{
    /// <summary>
    /// 示例插件，继承 <see cref="Plugin"/> 并实现基本功能
    /// 该插件在启动时输出一条日志消息
    /// </summary>
    public partial class HelloWorldPlugin : Plugin
    {
        private static HelloWorldPlugin? _instance;

        public override string Name => "HelloWorldPlugin";

        public override string Description => "A simple hello world plugin for demonstration purposes.";

        public override string Author => "Your Name";
        public override Version RequiredApiVersion => new Version(1, 0, 0);

#pragma warning disable CA2255
        [ModuleInitializer]
#pragma warning restore CA2255
        public static void Init()
        {
            
            SetPluginInfo(new HelloWorldPlugin());
        }

        /// <summary>
        /// 插件启动时调用的方法，输出欢迎日志
        /// </summary>
        protected override void OnStart()
        {
            Log.PrintLog("HelloWorldPlugin started. Hello, World!");
        }

        /// <summary>
        /// 插件导出的初始化入口点，由插件加载器通过 <c>OnInit</c> 名称调用
        /// </summary>
        /// <param name="hostApiTablePtr">指向宿主 API 函数表的非托管指针</param>
        /// <remarks>
        /// 此方法被 <see cref="UnmanagedCallersOnlyAttribute"/> 标记，
        /// 表示仅可由非托管代码调用。方法内部创建插件实例并调用
        /// <see cref="Plugin.Initialize"/> 完成初始化。
        /// </remarks>
        [UnmanagedCallersOnly(EntryPoint = "OnInit")]
        public static void OnInit(IntPtr hostApiTablePtr)
        {
            _instance = new HelloWorldPlugin();
            Initialize(hostApiTablePtr, _instance);
        }

        public override void OnStop()
        {
            _instance = null;
        }
    }
}
