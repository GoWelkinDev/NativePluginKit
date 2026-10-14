// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Loader.Plugins
{
    /// <summary>
    /// 标记一个 static 方法，该方法将在插件初始化时被注册为对外暴露的方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class PluginExportAttribute : Attribute
    {
        /// <summary>
        /// 对外暴露的方法名，缺省时使用 C# 方法名
        /// </summary>
        public string? Name { get; }

        public PluginExportAttribute(string? name = null) => Name = name;
    }
}
