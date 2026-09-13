using System.Runtime.InteropServices;
using static NativePluginKit.Loader.Constants.HostApiBridgeBuilder;

namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 主程序暴露给插件的函数指针表，包含插件可调用的宿主功能
    /// 结构体采用顺序布局，与非托管内存直接对应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HostApiTable
    {
        // ====== 基础设施 ======
        [HostApi("NativePluginKit.Features.Strings", "Strings", "Free",
            typeof(FreeStringDelegate), typeof(FreeStringDelegate))]
        public IntPtr FreeString;

        // ====== 场景 ======
        [HostApi("NativePluginKit.Features.FMOD", "Transition", "TransitionToScene",
            typeof(TransitionToSceneDelegate), typeof(TransitionToScenePublicDelegate))]
        public IntPtr TransitionToScene;

        [HostApi("NativePluginKit.Features.Scene", "Scene", "GetCurrentScenePath",
            typeof(GetStringDelegate), typeof(GetStringPublicDelegate))]
        public IntPtr Scene_GetCurrentScenePath;

        // ====== 日志 ======
        [HostApi("NativePluginKit.Features.Logger", "Log", "PrintLog",
            typeof(PrintLogDelegate), typeof(PrintLogPublicDelegate))]
        public IntPtr PrintLog;

        [HostApi("NativePluginKit.Features.Logger", "Log", "PrintWarning",
            typeof(PrintLogDelegate), typeof(PrintLogPublicDelegate))]
        public IntPtr PrintWarning;

        [HostApi("NativePluginKit.Features.Logger", "Log", "PrintError",
            typeof(PrintLogDelegate), typeof(PrintLogPublicDelegate))]
        public IntPtr PrintError;

        [HostApi("NativePluginKit.Features.Logger", "Log", "Debug",
            typeof(PrintLogDelegate), typeof(PrintLogPublicDelegate))]
        public IntPtr LogDebug;

        // ====== 配置 ======
        [HostApi("NativePluginKit.Features.Config", "Config", "GetCharacterImagePath",
            typeof(GetStringFromStringDelegate), typeof(GetStringFromStringPublicDelegate))]
        public IntPtr Config_GetCharacterImagePath;

        [HostApi("NativePluginKit.Features.Config", "Config", "GetExecutableDirectory",
            typeof(GetStringDelegate), typeof(GetStringPublicDelegate))]
        public IntPtr Config_GetExecutableDirectory;

        [HostApi("NativePluginKit.Features.Config", "Config", "GetPluginsDirectory",
            typeof(GetStringDelegate), typeof(GetStringPublicDelegate))]
        public IntPtr Config_GetPluginsDirectory;

        // ====== 计时 ======
        [HostApi("NativePluginKit.Features.Timing", "Timing", "GetTimeSinceStartup",
            typeof(GetDoubleDelegate), typeof(GetDoubleDelegate))]
        public IntPtr Timing_GetTimeSinceStartup;

        // ====== 事件 ======
        [HostApi("NativePluginKit.Features.Events", "EventBridge", "Register",
            typeof(RegisterEventDelegate), typeof(RegisterEventDelegate))]
        public IntPtr RegisterEventCallback;

        [HostApi("NativePluginKit.Features.Events", "EventBridge", "Unregister",
            typeof(UnregisterEventDelegate), typeof(UnregisterEventDelegate))]
        public IntPtr UnregisterEventCallback;

        // ====== 剧情 ======
        [HostApi("NativePluginKit.Features.Story", "Story", "GetCurrentTimelineName",
            typeof(GetStringDelegate), typeof(GetStringPublicDelegate))]
        public IntPtr Story_GetCurrentTimelineName;

        [HostApi("NativePluginKit.Features.Story", "Story", "IsBattle",
            typeof(GetBoolDelegate), typeof(GetBoolPublicDelegate))]
        public IntPtr Story_IsBattle;

        [HostApi("NativePluginKit.Features.Story", "Story", "StartTimeline",
            typeof(StartTimelineDelegate), typeof(StartTimelinePublicDelegate))]
        public IntPtr Story_StartTimeline;

        [HostApi("NativePluginKit.Features.Story", "Story", "EndTimeline",
            typeof(EndTimelineDelegate), typeof(EndTimelinePublicDelegate))]
        public IntPtr Story_EndTimeline;

        [HostApi("NativePluginKit.Features.Story", "Story", "SendSignal",
            typeof(PrintLogDelegate), typeof(PrintLogPublicDelegate))]
        public IntPtr Story_SendSignal;

        // ====== 卡牌基础 ======
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetCardCount",
            typeof(GetIntDelegate), typeof(GetIntDelegate))]
        public IntPtr Card_GetCardCount;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetCardNameAt",
            typeof(GetStringFromIntDelegate), typeof(GetStringFromIntPublicDelegate))]
        public IntPtr Card_GetCardNameAt;

        [HostApi("NativePluginKit.Features.Cards", "Card", "Contains",
            typeof(GetBoolFromStringDelegate), typeof(GetBoolFromStringPublicDelegate))]
        public IntPtr Card_Contains;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetDisplayName",
            typeof(GetStringFromStringDelegate), typeof(GetStringFromStringPublicDelegate))]
        public IntPtr Card_GetDisplayName;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetDescription",
            typeof(GetStringFromStringDelegate), typeof(GetStringFromStringPublicDelegate))]
        public IntPtr Card_GetDescription;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetImageName",
            typeof(GetStringFromStringDelegate), typeof(GetStringFromStringPublicDelegate))]
        public IntPtr Card_GetImageName;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetCost",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetCost;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetBaseAttack",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetBaseAttack;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetMaxStack",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetMaxStack;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetWeight",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetWeight;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAttackType",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetAttackType;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetCardType",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetCardType;

        // ====== 卡牌效果 ======
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectCount",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetAppliedEffectCount;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectId",
            typeof(GetStringFromStringIntDelegate), typeof(GetStringFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectId;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectName",
            typeof(GetStringFromStringIntDelegate), typeof(GetStringFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectName;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectCategory",
            typeof(GetIntFromStringIntDelegate), typeof(GetIntFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectCategory;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectDuration",
            typeof(GetIntFromStringIntDelegate), typeof(GetIntFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectDuration;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectIsStackable",
            typeof(GetBoolFromStringIntDelegate), typeof(GetBoolFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectIsStackable;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectMaxStacks",
            typeof(GetIntFromStringIntDelegate), typeof(GetIntFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectMaxStacks;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectParam1",
            typeof(GetFloatFromStringIntDelegate), typeof(GetFloatFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectParam1;

        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectParam2",
            typeof(GetFloatFromStringIntDelegate), typeof(GetFloatFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectParam2;

        // ====== 实体枚举与标识 ======
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEntityCount",
            typeof(GetIntDelegate), typeof(GetIntDelegate))]
        public IntPtr Entity_GetCount;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEntityIdAt",
            typeof(GetEntityIdAtDelegate), typeof(GetEntityIdAtPublicDelegate))]
        public IntPtr Entity_GetIdAt;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "Contains",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_Contains;

        // ====== 实体基础信息 ======
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetName",
            typeof(GetStringFromIdDelegate), typeof(GetStringFromIdPublicDelegate))]
        public IntPtr Entity_GetName;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetDisplayName",
            typeof(GetStringFromIdDelegate), typeof(GetStringFromIdPublicDelegate))]
        public IntPtr Entity_GetDisplayName;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetImagePath",
            typeof(GetStringFromIdDelegate), typeof(GetStringFromIdPublicDelegate))]
        public IntPtr Entity_GetImagePath;

        // ====== 实体数值属性 ======
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetMaxHP",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetMaxHP;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetCurrentHP",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetCurrentHP;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetAttackPower",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetAttackPower;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetDodgeChance",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetDodgeChance;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetShieldValue",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetShieldValue;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetArmorValue",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetArmorValue;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetCurrentCost",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetCurrentCost;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetMaxCost",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetMaxCost;

        // ====== 实体状态 ======
        [HostApi("NativePluginKit.Features.Entities", "Entity", "IsAlive",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_IsAlive;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "IsDead",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_IsDead;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "CanAct",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_CanAct;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "IsPlayer",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_IsPlayer;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "IsMob",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_IsMob;

        // ====== 实体效果 ======
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectCount",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetEffectCount;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectId",
            typeof(GetStringFromIdIntDelegate), typeof(GetStringFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectId;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectName",
            typeof(GetStringFromIdIntDelegate), typeof(GetStringFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectName;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectCategory",
            typeof(GetIntFromIdIntDelegate), typeof(GetIntFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectCategory;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectRemainingDuration",
            typeof(GetIntFromIdIntDelegate), typeof(GetIntFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectRemainingDuration;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectCurrentStacks",
            typeof(GetIntFromIdIntDelegate), typeof(GetIntFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectCurrentStacks;

        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectMaxStacks",
            typeof(GetIntFromIdIntDelegate), typeof(GetIntFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectMaxStacks;
    }

    /// <summary>
    /// 插件导出的初始化函数签名
    /// 插件必须导出一个名为 <c>OnInit</c> 的函数，参数为指向 <see cref="HostApiTable"/> 的指针
    /// </summary>
    /// <param name="hostApiTable">指向宿主API函数表的指针</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PluginInitDelegate(IntPtr hostApiTable);
}
