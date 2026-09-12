using NativePluginKit.Features.Logger;
using NativePluginKit.Loader.Plugins;

namespace HelloWorldPlugin
{
    /// <summary>
    /// 示例插件，继承 <see cref="Plugin"/> 并实现基本功能
    /// 该插件在启动时输出一条日志消息
    /// </summary>
    public partial class HelloWorldPlugin : Plugin
    {
        public override string Name => "HelloWorldPlugin";

        public override string Description => "A simple hello world plugin for demonstration purposes.";

        public override string Author => "Your Name";
        public override Version RequiredApiVersion => new Version(1, 0, 0);

        /// <summary>
        /// 插件启动时调用的方法，输出欢迎日志
        /// </summary>
        protected override void OnStart()
        {
            Log.PrintLog("HelloWorldPlugin started. Hello, World!");
        }

        public override void OnStop()
        {
            Log.PrintLog("HelloWorldPlugin stopped. Goodbye!");
        }
    }
}
