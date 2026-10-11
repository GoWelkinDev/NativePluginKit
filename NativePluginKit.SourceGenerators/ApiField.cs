using Microsoft.CodeAnalysis;

namespace NativePluginKit.SourceGenerators
{
    /// <summary>
    /// 描述一个标注了 [HostApi] 的字段的元数据
    /// </summary>
    internal readonly struct ApiField
    {
        public string FieldName { get; }
        public string Namespace { get; }
        public string ClassName { get; }
        public string MethodName { get; }
        public INamedTypeSymbol NativeDelegate { get; }
        public INamedTypeSymbol PublicDelegate { get; }
        public string? Comment { get; }

        public ApiField(
            string fieldName,
            string ns,
            string className,
            string methodName,
            INamedTypeSymbol nativeDelegate,
            INamedTypeSymbol publicDelegate,
            string? comment)
        {
            FieldName = fieldName;
            Namespace = ns;
            ClassName = className;
            MethodName = methodName;
            NativeDelegate = nativeDelegate;
            PublicDelegate = publicDelegate;
            Comment = comment;
        }
    }
}
