using NativePluginKit.Features.Wrappers;
using System.Runtime.InteropServices;

namespace NativePluginKit.Events
{
    /// <summary>
    /// 每个事件数据的前 8 字节固定为此结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EventHeader
    {
        public int EventType;
        public int DataSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PluginLoadedEvent
    {
        public EventHeader Header;
        public IntPtr PluginNamePtr;   // UTF-8
        public IntPtr VersionPtr;      // UTF-8
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SceneEvent
    {
        public EventHeader Header;
        public IntPtr ScenePathPtr;    // UTF-8
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TimelineEvent
    {
        public EventHeader Header;
        public IntPtr TimelineNamePtr; // UTF-8
        public bool IsBattle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BattleMessageEvent
    {
        public EventHeader Header;
        public IntPtr MessagePtr;      // UTF-8
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TurnEvent
    {
        public EventHeader Header;
        public int Turn;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CardEvent
    {
        public EventHeader Header;
        public ObjectIdNative OwnerEntityId;
        public IntPtr CardNamePtr;     // UTF-8，CardData.Name
        public ObjectIdNative TargetEntityId;   // 仅在 CardPlayed 时有效
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EntityDamagedEvent
    {
        public EventHeader Header;
        public ObjectIdNative EntityId;
        public int RawDamage;
        public int HpDamage;
        public int AttackType;         // AttackType enum
        public int RemainingHP;
        public bool Dodged;
        public int ShieldAbsorbed;
        public int ArmorAbsorbed;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EntityDiedEvent
    {
        public EventHeader Header;
        public ObjectIdNative EntityId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EntityUpdatedEvent
    {
        public EventHeader Header;
        public ObjectIdNative EntityId;
        public int CurrentHP;
        public int CurrentCost;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EffectEvent
    {
        public EventHeader Header;
        public ObjectIdNative OwnerEntityId;
        public IntPtr EffectIdPtr;     // UTF-8
        public IntPtr EffectNamePtr;   // UTF-8
        public int RemainingDuration;
        public int CurrentStacks;
    }
}