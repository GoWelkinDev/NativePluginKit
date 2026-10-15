// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Loader.Constants
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class HostExportAttribute : Attribute
    {
        public string GroupName { get; }

        public HostExportAttribute(string groupName) => GroupName = groupName;
    }
}
