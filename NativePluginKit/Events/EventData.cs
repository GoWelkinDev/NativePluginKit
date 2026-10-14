// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Features.Wrappers;
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

    /// <summary>
    /// 插件加载完成后触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PluginLoadedEvent
    {
        public EventHeader Header;
        public IntPtr PluginNamePtr;
        public IntPtr VersionPtr;

        public readonly string PluginName => StringNative.FromPtr(PluginNamePtr);
        public readonly string Version => StringNative.FromPtr(VersionPtr);
    }

    /// <summary>
    /// 场景切换时触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SceneEvent
    {
        public EventHeader Header;
        public IntPtr ScenePathPtr;

        public readonly string ScenePath => StringNative.FromPtr(ScenePathPtr);
    }

    /// <summary>
    /// 剧情时间线开始或结束时触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TimelineEvent
    {
        public EventHeader Header;
        public IntPtr TimelineNamePtr;
        public bool IsBattle;

        public readonly string TimelineName => StringNative.FromPtr(TimelineNamePtr);
    }

    /// <summary>
    /// 剧情时间线信号触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TimelineSignalEvent
    {
        public EventHeader Header;
        public IntPtr SignalNamePtr;

        public readonly string SignalName => StringNative.FromPtr(SignalNamePtr);
    }

    /// <summary>
    /// 战斗开始或结束时触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct BattleEvent
    {
        public EventHeader Header;
        public int Victory;
    }

    /// <summary>
    /// 战斗过程中产生的消息事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct BattleMessageEvent
    {
        public EventHeader Header;
        public IntPtr MessagePtr;

        public readonly string Message => StringNative.FromPtr(MessagePtr);
    }

    /// <summary>
    /// 回合开始或结束时触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TurnEvent
    {
        public EventHeader Header;

        public int Turn;
    }

    /// <summary>
    /// 卡牌被抽到请求打出打出或弃置时触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CardEvent
    {
        public EventHeader Header;

        public ObjectIdNative OwnerEntityId;
        public IntPtr CardNamePtr;
        public ObjectIdNative TargetEntityId;

        public readonly EntityId Owner => OwnerEntityId.ToEntityId();
        public readonly string CardName => StringNative.FromPtr(CardNamePtr);
        public readonly EntityId Target => TargetEntityId.ToEntityId();
    }

    /// <summary>
    /// 实体受到伤害时触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EntityDamagedEvent
    {
        public EventHeader Header;
        public ObjectIdNative EntityId;
        public int RawDamage;
        public int HpDamage;
        public int AttackType;
        public int RemainingHP;
        public bool Dodged;
        public int ShieldAbsorbed;
        public int ArmorAbsorbed;

        public readonly EntityId Entity => EntityId.ToEntityId();
    }

    /// <summary>
    /// 实体死亡时触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EntityDiedEvent
    {
        public EventHeader Header;
        public ObjectIdNative EntityId;

        public readonly EntityId Entity => EntityId.ToEntityId();
    }

    /// <summary>
    /// 实体属性发生变化时触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EntityUpdatedEvent
    {
        public EventHeader Header;
        public ObjectIdNative EntityId;
        public int CurrentHP;
        public int CurrentCost;

        public readonly EntityId Entity => EntityId.ToEntityId();
    }

    /// <summary>
    /// 效果被施加或到期时触发的事件载荷
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EffectEvent
    {
        public EventHeader Header;
        public ObjectIdNative OwnerEntityId;
        public IntPtr EffectIdPtr;
        public IntPtr EffectNamePtr;
        public int RemainingDuration;
        public int CurrentStacks;

        public readonly EntityId Owner => OwnerEntityId.ToEntityId();
        public readonly string EffectId => StringNative.FromPtr(EffectIdPtr);
        public readonly string EffectName => StringNative.FromPtr(EffectNamePtr);
    }
}