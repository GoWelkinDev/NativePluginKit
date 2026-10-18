// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

namespace NativePluginKit.Features.Events
{
    /// <summary>
    /// 事件类型标识
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 插件加载完成
        /// </summary>
        PluginLoaded = 100,

        /// <summary>
        /// 插件已卸载
        /// </summary>
        PluginUnloaded = 101,

        /// <summary>
        /// 场景即将切换
        /// </summary>
        SceneChanging = 200,

        /// <summary>
        /// 场景已切换
        /// </summary>
        SceneChanged = 201,

        /// <summary>
        /// 剧情时间线开始
        /// </summary>
        TimelineStarted = 300,

        /// <summary>
        /// 剧情时间线结束
        /// </summary>
        TimelineEnded = 301,

        /// <summary>
        /// 剧情时间线信号
        /// </summary>
        TimelineSignal = 302,

        /// <summary>
        /// 战斗开始
        /// </summary>
        BattleStarted = 400,

        /// <summary>
        /// 战斗结束
        /// </summary>
        BattleEnded = 401,

        /// <summary>
        /// 回合开始
        /// </summary>
        TurnStarted = 402,

        /// <summary>
        /// 回合结束
        /// </summary>
        TurnEnded = 403,

        /// <summary>
        /// 战斗消息
        /// </summary>
        BattleMessage = 404,

        /// <summary>
        /// 卡牌被抽到
        /// </summary>
        CardDrawn = 500,

        /// <summary>
        /// 卡牌请求打出
        /// </summary>
        CardPlayRequested = 501,

        /// <summary>
        /// 卡牌已打出
        /// </summary>
        CardPlayed = 502,

        /// <summary>
        /// 卡牌被弃置
        /// </summary>
        CardDiscarded = 503,

        /// <summary>
        /// 实体受到伤害
        /// </summary>
        EntityDamaged = 600,

        /// <summary>
        /// 实体被治疗
        /// </summary>
        EntityHealed = 601,

        /// <summary>
        /// 实体死亡
        /// </summary>
        EntityDied = 602,

        /// <summary>
        /// 实体属性更新
        /// </summary>
        EntityUpdated = 603,

        /// <summary>
        /// 效果被施加
        /// </summary>
        EffectApplied = 700,

        /// <summary>
        /// 效果已过期
        /// </summary>
        EffectExpired = 701,
    }
}