using System.Runtime.InteropServices;

namespace NativePluginKit.Features.Wrappers
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ObjectIdNative
    {
        public ulong Low;
        public ulong High;
    }
}
