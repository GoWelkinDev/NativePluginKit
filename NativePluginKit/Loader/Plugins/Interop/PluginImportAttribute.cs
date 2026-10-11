namespace NativePluginKit.Loader.Plugins.Interop
{
    /// <summary>
    /// 声明一个 static partial 方法以作为导入其他插件方法的占位符
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class PluginImportAttribute : Attribute
    {
        public string PluginName { get; }
        public string MethodName { get; }

        /// <summary>
        /// 该变量为 true 时若目标方法未注册则调用抛出 InvalidOperationException
        /// 否则返回类型默认值并记录一次错误日志
        /// </summary>
        public bool Required { get; init; }

        public PluginImportAttribute(string pluginName, string methodName)
        {
            PluginName = pluginName;
            MethodName = methodName;
        }
    }
}
