// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using System.Runtime.InteropServices;

namespace NativePluginKit.Events
{
    /// <summary>
    /// 每个事件数据的前 8 字节固定为此结构
    /// 用于描述事件类型与载荷大小
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EventHeader
    {
        public int EventType;
        public int DataSize;
    }
}
