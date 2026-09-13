namespace NativePluginKit.Events
{
    /// <summary>
    /// 事件类型标识。数值一旦发布不可更改。
    /// </summary>
    public enum EventType
    {
        // 1xx — 插件生命周期
        PluginLoaded = 100,
        PluginUnloaded = 101,

        // 2xx — 场景
        SceneChanging = 200,
        SceneChanged = 201,

        // 3xx — 剧情
        TimelineStarted = 300,
        TimelineEnded = 301,
        TimelineSignal = 302,

        // 4xx — 战斗
        BattleStarted = 400,
        BattleEnded = 401,
        TurnStarted = 402,
        TurnEnded = 403,
        BattleMessage = 404,

        // 5xx — 卡牌
        CardDrawn = 500,
        CardPlayRequested = 501,
        CardPlayed = 502,
        CardDiscarded = 503,

        // 6xx — 实体
        EntityDamaged = 600,
        EntityHealed = 601,
        EntityDied = 602,
        EntityUpdated = 603,

        // 7xx — 效果
        EffectApplied = 700,
        EffectExpired = 701,
    }
}