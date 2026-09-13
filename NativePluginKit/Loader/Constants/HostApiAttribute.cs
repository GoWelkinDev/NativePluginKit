namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 标注 <see cref="HostApiTable"/> 中的字段，指示源生成器为其生成公共 API 类
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class HostApiAttribute : Attribute
    {
        /// <summary>
        /// 目标命名空间，例如 NativePluginKit.Features.Logger
        /// </summary>
        public string TargetNamespace { get; }

        /// <summary>
        /// 目标类名，例如 Log
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// 目标方法名，例如 PrintLog
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// 原生签名委托（参数为 IntPtr 等非托管类型）
        /// </summary>
        public Type NativeDelegate { get; }

        /// <summary>
        /// 公共签名委托（参数为 string 等托管类型），可选，缺省时与原生一致
        /// </summary>
        public Type? PublicDelegate { get; }

        public HostApiAttribute(
            string targetNamespace,
            string className,
            string methodName,
            Type nativeDelegate,
            Type? publicDelegate = null)
        {
            TargetNamespace = targetNamespace;
            ClassName = className;
            MethodName = methodName;
            NativeDelegate = nativeDelegate;
            PublicDelegate = publicDelegate ?? nativeDelegate;
        }
    }
}
