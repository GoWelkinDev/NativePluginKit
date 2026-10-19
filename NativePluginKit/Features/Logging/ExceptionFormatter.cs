// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using System.Text;

namespace NativePluginKit.Features.Logging
{
    /// <summary>
    /// 异常格式化工具
    /// </summary>
    internal static class ExceptionFormatter
    {
        public static string Format(Exception ex)
            => StripOffsets(ReplaceTypeHandle(ex.ToString()));

        private static string ReplaceTypeHandle(string text)
        {
            int lineEnd = text.IndexOf('\n');
            if (lineEnd < 0) lineEnd = text.Length;

            if (lineEnd >= 2 && text[0] == '0' && text[1] == 'x')
            {
                int colon = text.IndexOf(':', 0, lineEnd);
                if (colon > 0)
                    return string.Concat(nameof(Exception), text.AsSpan(colon));
            }
            return text;
        }

        private static string StripOffsets(string text)
        {
            var sb = new StringBuilder(text.Length);
            foreach (var raw in text.Split('\n'))
            {
                var line = raw.TrimEnd('\r');
                var idx = line.LastIndexOf(" + 0x", StringComparison.Ordinal);
                if (idx >= 0) line = line.Substring(0, idx);
                sb.AppendLine(line);
            }
            return sb.ToString();
        }
    }
}
