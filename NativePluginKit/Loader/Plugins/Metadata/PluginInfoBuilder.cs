// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NativePluginKit.Loader.Plugins.Metadata
{
    /// <summary>
    /// 构建与释放 PluginInfo 结构体及其字符串占用的非托管内存
    /// </summary>
    public static class PluginInfoBuilder
    {
        /// <summary>
        /// 构建 PluginInfo 并返回其非托管指针
        /// </summary>
        public static IntPtr Build(
            string name,
            string description,
            string author,
            Version version,
            Version requiredApi)
        {
            var info = new PluginInfo
            {
                NamePtr = Marshal.StringToCoTaskMemUTF8(name),
                DescriptionPtr = Marshal.StringToCoTaskMemUTF8(description),
                AuthorPtr = Marshal.StringToCoTaskMemUTF8(author),
                MajorVersion = Math.Max(version.Major, 0),
                MinorVersion = Math.Max(version.Minor, 0),
                BuildVersion = Math.Max(version.Build, 0),
                RequiredApiMajor = Math.Max(requiredApi.Major, 0),
                RequiredApiMinor = Math.Max(requiredApi.Minor, 0),
                RequiredApiBuild = Math.Max(requiredApi.Build, 0),
            };

            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf<PluginInfo>());
            Marshal.StructureToPtr(info, ptr, false);
            return ptr;
        }

        /// <summary>
        /// 释放 PluginInfo 及其字符串占用的非托管内存
        /// </summary>
        public static unsafe void Free(ref IntPtr ptr)
        {
            if (ptr == IntPtr.Zero) return;

            var info = Unsafe.Read<PluginInfo>((void*)ptr);
            if (info.NamePtr != IntPtr.Zero) Marshal.FreeCoTaskMem(info.NamePtr);
            if (info.DescriptionPtr != IntPtr.Zero) Marshal.FreeCoTaskMem(info.DescriptionPtr);
            if (info.AuthorPtr != IntPtr.Zero) Marshal.FreeCoTaskMem(info.AuthorPtr);
            Marshal.FreeHGlobal(ptr);
            ptr = IntPtr.Zero;
        }
    }
}
