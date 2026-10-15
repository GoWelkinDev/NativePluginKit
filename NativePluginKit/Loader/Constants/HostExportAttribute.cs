// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 标记一个宿主侧的 static 方法，指示源生成器为其生成导出方法，供插件调用
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class HostExportAttribute : Attribute
    {
        public string GroupName { get; }

        public HostExportAttribute(string groupName) => GroupName = groupName;
    }
}
