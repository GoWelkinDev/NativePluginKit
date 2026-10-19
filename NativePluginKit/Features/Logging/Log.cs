// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Loader.Plugins;
using NativePluginKit.Loader.Plugins.NativeInterop;

namespace NativePluginKit.Features.Logging
{
    /// <summary>
    /// 宿主日志记录
    /// </summary>
    public static class Log
    {
        private static readonly NativeCall Host = NativeCalls.Get("__host__:Logging");

        /// <summary>
        /// Info 级别日志
        /// </summary>
        public static void PrintLog(string message)
            => Write(nameof(PrintLog), message);

        /// <summary>
        /// Warning 级别日志
        /// </summary>
        public static void PrintWarning(string message)
            => Write(nameof(PrintWarning), message);

        /// <summary>
        /// Error 级别日志
        /// </summary>
        public static void PrintError(string message)
            => Write(nameof(PrintError), message);

        /// <summary>
        /// Debug 级别日志
        /// </summary>
        public static void Debug(string message)
            => Write(nameof(Debug), message);

        /// <summary>
        /// Error 级别日志，自动格式化异常
        /// </summary>
        public static void PrintError(Exception ex, string? category = null)
            => Write(nameof(PrintError), FormatException(ex, category));

        /// <summary>
        /// Warning 级别日志，自动格式化异常
        /// </summary>
        public static void PrintWarning(Exception ex, string? category = null)
            => Write(nameof(PrintWarning), FormatException(ex, category));

        private static void Write(string method, string message)
        {
            var prefixed = string.Concat("[", PluginContext.PluginName, "] ", message);
            Host.Invoke(method, prefixed);
        }

        private static string FormatException(Exception ex, string? category)
        {
            var formatted = ExceptionFormatter.Format(ex);
            return category is null ? formatted : string.Concat("[", category, "] ", formatted);
        }
    }
}
