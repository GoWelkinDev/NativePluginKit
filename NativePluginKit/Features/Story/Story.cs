// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Loader.Plugins.NativeInterop;

namespace NativePluginKit.Features.Story
{
    /// <summary>
    /// 剧情时间线状态查询
    /// </summary>
    public static class Story
    {
        private static readonly NativeCall Host = NativeCalls.Get("__host__:Story");

        /// <summary>
        /// 返回当前剧情时间线名称
        /// </summary>
        public static string GetCurrentTimelineName()
            => Host.Invoke<string>(nameof(GetCurrentTimelineName));

        /// <summary>
        /// 返回当前是否处于战斗状态
        /// </summary>
        public static bool IsBattle()
            => Host.Invoke<bool>(nameof(IsBattle));
    }
}
