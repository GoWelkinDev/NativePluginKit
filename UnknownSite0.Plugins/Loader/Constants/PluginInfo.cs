using System.Runtime.InteropServices;

namespace UnknownSite0.Plugins.Loader.Constants
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginInfo
    {
        // 使用 UTF-8 字符串指针，插件负责提供有效指针（建议指向静态只读内存）
        public IntPtr NamePtr;
        public IntPtr DescriptionPtr;
        public IntPtr AuthorPtr;

        // 版本号使用两个 int 表示主版本和次版本，避免跨 ABI 传递 System.Version 对象
        public int MajorVersion;
        public int MinorVersion;
        public int BuildVersion;
        public int RevisionVersion;

        // 所需 API 版本
        public int RequiredApiMajor;
        public int RequiredApiMinor;

        // 将指针转换为托管字符串的辅助方法（在主程序端使用）
        public string GetName() => Marshal.PtrToStringUTF8(NamePtr) ?? string.Empty;
        public string GetDescription() => Marshal.PtrToStringUTF8(DescriptionPtr) ?? string.Empty;
        public string GetAuthor() => Marshal.PtrToStringUTF8(AuthorPtr) ?? string.Empty;
        public Version GetVersion() => new Version(MajorVersion, MinorVersion, BuildVersion, RevisionVersion);
        public Version GetRequiredApiVersion() => new Version(RequiredApiMajor, RequiredApiMinor);
    }
}
