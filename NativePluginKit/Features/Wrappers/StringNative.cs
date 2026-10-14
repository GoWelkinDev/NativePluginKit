// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using System.Runtime.InteropServices;

namespace NativePluginKit.Features.Wrappers
{
    /// <summary>
    /// 非托管 UTF-8 字符串指针的托管侧读取入口
    /// </summary>
    public static class StringNative
    {
        /// <summary>
        /// 读取非托管 UTF-8 字符串指针
        /// </summary>
        /// <param name="ptr">由宿主分配的非托管字符串指针</param>
        /// <returns>解码后的托管字符串指针，为零时返回空字符串</returns>
        public static string FromPtr(IntPtr ptr)
            => ptr == IntPtr.Zero ? string.Empty : Marshal.PtrToStringUTF8(ptr) ?? string.Empty;
    }
}
