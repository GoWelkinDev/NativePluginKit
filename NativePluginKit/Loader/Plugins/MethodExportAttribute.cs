// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Loader.Plugins
{
    /// <summary>
    /// 标记一个 static 方法，该方法将在初始化时被注册为对外暴露的方法
    /// </summary>
    /// <remarks>
    /// 注册键为 <c>PluginContext.PluginName</c>，指定 <see cref="Group"/> 时追加 <c>":{GroupName}"</c>
    /// <para>宿主启动时需将 <c>PluginContext.PluginName</c> 设为 <c>"__host__"</c></para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class MethodExportAttribute : Attribute
    {
        /// <summary>
        /// 可选分组名，为 <c>null</c> 时注册到根命名空间下
        /// </summary>
        public string? Group { get; init; }
    }
}
