// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

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
