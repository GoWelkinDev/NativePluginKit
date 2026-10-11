using System.Runtime.InteropServices;
using static NativePluginKit.Loader.Constants.HostApiBridgeBuilder;

#pragma warning disable CS1572
namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 主程序暴露给插件的函数指针表，包含插件可调用的宿主功能
    /// 结构体采用顺序布局，与非托管内存直接对应
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HostApiTable
    {
        /// <summary>
        /// 释放由宿主分配的非托管 UTF-8 字符串
        /// </summary>
        /// <param name="ptr">要释放的非托管字符串指针</param>
        [HostApi("NativePluginKit.Features.Strings", "Strings", "Free",
            typeof(FreeStringDelegate), typeof(FreeStringDelegate))]
        public IntPtr FreeString;

        /// <summary>
        /// 将当前插件导出的方法注册到宿主插件方法表供其他插件调用
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <param name="methodName">对外暴露的方法名</param>
        /// <param name="fnPtr">方法对应的非托管函数指针</param>
        /// <returns>注册成功返回 1, 否则返回 0</returns>
        [HostApi("NativePluginKit.Features.Plugins", "PluginMethods", "Register",
            typeof(RegisterPluginMethodDelegate), typeof(RegisterPluginMethodPublicDelegate))]
        public IntPtr PluginMethods_Register;

        /// <summary>
        /// 从宿主插件方法表移除指定方法
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <param name="methodName">要移除的方法名</param>
        /// <returns>移除成功返回 1, 否则返回 0</returns>
        [HostApi("NativePluginKit.Features.Plugins", "PluginMethods", "Unregister",
            typeof(UnregisterPluginMethodDelegate), typeof(UnregisterPluginMethodPublicDelegate))]
        public IntPtr PluginMethods_Unregister;

        /// <summary>
        /// 查询其他插件已注册的方法指针
        /// </summary>
        /// <param name="pluginName">目标插件名称</param>
        /// <param name="methodName">目标方法名</param>
        /// <returns>方法对应的函数指针未找到时返回 <see cref="IntPtr.Zero"/></returns>
        [HostApi("NativePluginKit.Features.Plugins", "PluginMethods", "Get",
            typeof(GetPluginMethodDelegate), typeof(GetPluginMethodPublicDelegate))]
        public IntPtr PluginMethods_Get;

        /// <summary>
        /// 返回当前场景资源路径，若无则为空字符串
        /// </summary>
        [HostApi("NativePluginKit.Features.Scene", "Scene", "GetCurrentScenePath",
            typeof(GetStringDelegate), typeof(GetStringPublicDelegate))]
        public IntPtr Scene_GetCurrentScenePath;

        /// <summary>
        /// Info 级别日志
        /// </summary>
        [HostApi("NativePluginKit.Features.Logger", "Log", "PrintLog",
            typeof(PrintLogDelegate), typeof(PrintLogPublicDelegate))]
        public IntPtr PrintLog;

        /// <summary>
        /// Warning 级别日志
        /// </summary>
        [HostApi("NativePluginKit.Features.Logger", "Log", "PrintWarning",
            typeof(PrintLogDelegate), typeof(PrintLogPublicDelegate))]
        public IntPtr PrintWarning;

        /// <summary>
        /// Error 级别日志
        /// </summary>
        [HostApi("NativePluginKit.Features.Logger", "Log", "PrintError",
            typeof(PrintLogDelegate), typeof(PrintLogPublicDelegate))]
        public IntPtr PrintError;

        /// <summary>
        /// Debug 级别日志
        /// </summary>
        [HostApi("NativePluginKit.Features.Logger", "Log", "Debug",
            typeof(PrintLogDelegate), typeof(PrintLogPublicDelegate))]
        public IntPtr LogDebug;

        /// <summary>
        /// 返回宿主指定的插件目录路径
        /// </summary>
        [HostApi("NativePluginKit.Features.Config", "Config", "GetPluginsDirectory",
            typeof(GetStringDelegate), typeof(GetStringPublicDelegate))]
        public IntPtr Config_GetPluginsDirectory;

        /// <summary>
        /// 返回自宿主启动以来经过的秒数
        /// </summary>
        [HostApi("NativePluginKit.Features.Timing", "Timing", "GetTimeSinceStartup",
            typeof(GetDoubleDelegate), typeof(GetDoubleDelegate))]
        public IntPtr Timing_GetTimeSinceStartup;

        /// <summary>
        /// 注册事件回调
        /// </summary>
        /// <param name="eventType">事件类型对应 <c>EventType</c> 的数值</param>
        /// <param name="callback">回调函数指针签名为 <c>void(int, IntPtr, IntPtr)</c></param>
        /// <param name="userData">用户自定义数据会原样传回回调</param>
        /// <returns>注册成功返回 true</returns>
        [HostApi("NativePluginKit.Features.Events", "EventBridge", "Register",
            typeof(RegisterEventDelegate), typeof(RegisterEventDelegate))]
        public IntPtr RegisterEventCallback;

        /// <summary>
        /// 注销事件回调
        /// </summary>
        /// <param name="eventType">事件类型对应 <c>EventType</c> 的数值</param>
        /// <param name="callback">回调函数指针</param>
        /// <param name="userData">注册时传入的用户自定义数据</param>
        /// <returns>取消成功返回 true</returns>
        [HostApi("NativePluginKit.Features.Events", "EventBridge", "Unregister",
            typeof(UnregisterEventDelegate), typeof(UnregisterEventDelegate))]
        public IntPtr UnregisterEventCallback;

        /// <summary>
        /// 返回当前剧情时间线名称
        /// </summary>
        [HostApi("NativePluginKit.Features.Story", "Story", "GetCurrentTimelineName",
            typeof(GetStringDelegate), typeof(GetStringPublicDelegate))]
        public IntPtr Story_GetCurrentTimelineName;

        /// <summary>
        /// 返回当前是否处于战斗状态
        /// </summary>
        [HostApi("NativePluginKit.Features.Story", "Story", "IsBattle",
            typeof(GetBoolDelegate), typeof(GetBoolPublicDelegate))]
        public IntPtr Story_IsBattle;

        /// <summary>
        /// 返回卡牌总数
        /// </summary>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetCardCount",
            typeof(GetIntDelegate), typeof(GetIntDelegate))]
        public IntPtr Card_GetCardCount;

        /// <summary>
        /// 根据索引获取卡牌名
        /// </summary>
        /// <param name="index">卡牌索引</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetCardNameAt",
            typeof(GetStringFromIntDelegate), typeof(GetStringFromIntPublicDelegate))]
        public IntPtr Card_GetCardNameAt;

        /// <summary>
        /// 判断指定名称的卡牌是否存在
        /// </summary>
        /// <param name="name">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "Contains",
            typeof(GetBoolFromStringDelegate), typeof(GetBoolFromStringPublicDelegate))]
        public IntPtr Card_Contains;

        /// <summary>
        /// 返回卡牌的显示名称
        /// </summary>
        /// <param name="imageName">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetDisplayName",
            typeof(GetStringFromStringDelegate), typeof(GetStringFromStringPublicDelegate))]
        public IntPtr Card_GetDisplayName;

        /// <summary>
        /// 返回卡牌描述
        /// </summary>
        /// <param name="imageName">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetDescription",
            typeof(GetStringFromStringDelegate), typeof(GetStringFromStringPublicDelegate))]
        public IntPtr Card_GetDescription;

        /// <summary>
        /// 返回卡牌图片资源名称
        /// </summary>
        /// <param name="imageName">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetImageName",
            typeof(GetStringFromStringDelegate), typeof(GetStringFromStringPublicDelegate))]
        public IntPtr Card_GetImageName;

        /// <summary>
        /// 返回卡牌费用
        /// </summary>
        /// <param name="name">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetCost",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetCost;

        /// <summary>
        /// 返回卡牌基础攻击力
        /// </summary>
        /// <param name="name">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetBaseAttack",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetBaseAttack;

        /// <summary>
        /// 返回卡牌最大堆叠数
        /// </summary>
        /// <param name="name">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetMaxStack",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetMaxStack;

        /// <summary>
        /// 返回卡牌权重
        /// </summary>
        /// <param name="name">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetWeight",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetWeight;

        /// <summary>
        /// 返回卡牌攻击类型对应 <c>CardAttackType</c> 的数值
        /// </summary>
        /// <param name="name">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAttackType",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetAttackType;

        /// <summary>
        /// 返回卡牌类型对应 <c>CardCategory</c> 的数值
        /// </summary>
        /// <param name="name">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetCardType",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetCardType;

        /// <summary>
        /// 返回卡牌附加效果的数量
        /// </summary>
        /// <param name="name">卡牌名称</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectCount",
            typeof(GetIntFromStringDelegate), typeof(GetIntFromStringPublicDelegate))]
        public IntPtr Card_GetAppliedEffectCount;

        /// <summary>
        /// 返回卡牌附加效果的 ID
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectId",
            typeof(GetStringFromStringIntDelegate), typeof(GetStringFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectId;

        /// <summary>
        /// 返回卡牌附加效果名称
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectName",
            typeof(GetStringFromStringIntDelegate), typeof(GetStringFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectName;

        /// <summary>
        /// 返回卡牌附加效果类别对应 <c>EffectCategory</c> 的数值
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectCategory",
            typeof(GetIntFromStringIntDelegate), typeof(GetIntFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectCategory;

        /// <summary>
        /// 返回卡牌附加效果的持续时间
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectDuration",
            typeof(GetIntFromStringIntDelegate), typeof(GetIntFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectDuration;

        /// <summary>
        /// 返回卡牌附加效果是否可堆叠
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectIsStackable",
            typeof(GetBoolFromStringIntDelegate), typeof(GetBoolFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectIsStackable;

        /// <summary>
        /// 返回卡牌附加效果的最大堆叠数
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectMaxStacks",
            typeof(GetIntFromStringIntDelegate), typeof(GetIntFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectMaxStacks;

        /// <summary>
        /// 返回卡牌附加效果的参数 1
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectParam1",
            typeof(GetFloatFromStringIntDelegate), typeof(GetFloatFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectParam1;

        /// <summary>
        /// 返回卡牌附加效果的参数 2
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Cards", "Card", "GetAppliedEffectParam2",
            typeof(GetFloatFromStringIntDelegate), typeof(GetFloatFromStringIntPublicDelegate))]
        public IntPtr Card_GetAppliedEffectParam2;

        /// <summary>
        /// 返回当前场景中的实体数量
        /// </summary>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEntityCount",
            typeof(GetIntDelegate), typeof(GetIntDelegate))]
        public IntPtr Entity_GetCount;

        /// <summary>
        /// 读取指定索引实体的 ID 到 16 字节缓冲区，低 64 位在前，高 64 位在后
        /// </summary>
        /// <param name="index">实体索引</param>
        /// <param name="out16Bytes">输出缓冲区指针至少需要 16 字节</param>
        /// <returns>成功返回 1, 否则返回 0</returns>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEntityIdAt",
            typeof(GetEntityIdAtDelegate), typeof(GetEntityIdAtPublicDelegate))]
        public IntPtr Entity_GetIdAt;

        /// <summary>
        /// 判断实体是否仍然存在
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "Contains",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_Contains;

        /// <summary>
        /// 返回实体内部名称
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetName",
            typeof(GetStringFromIdDelegate), typeof(GetStringFromIdPublicDelegate))]
        public IntPtr Entity_GetName;

        /// <summary>
        /// 返回实体显示名称
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetDisplayName",
            typeof(GetStringFromIdDelegate), typeof(GetStringFromIdPublicDelegate))]
        public IntPtr Entity_GetDisplayName;

        /// <summary>
        /// 返回实体图片路径
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetImagePath",
            typeof(GetStringFromIdDelegate), typeof(GetStringFromIdPublicDelegate))]
        public IntPtr Entity_GetImagePath;

        /// <summary>
        /// 返回实体最大生命值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetMaxHP",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetMaxHP;

        /// <summary>
        /// 返回实体当前生命值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetCurrentHP",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetCurrentHP;

        /// <summary>
        /// 返回实体攻击力
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetAttackPower",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetAttackPower;

        /// <summary>
        /// 返回实体闪避值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetDodgeChance",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetDodgeChance;

        /// <summary>
        /// 返回实体护盾值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetShieldValue",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetShieldValue;

        /// <summary>
        /// 返回实体护甲值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetArmorValue",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetArmorValue;

        /// <summary>
        /// 返回实体当前费用
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetCurrentCost",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetCurrentCost;

        /// <summary>
        /// 返回实体最大费用
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetMaxCost",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetMaxCost;

        /// <summary>
        /// 返回实体是否存活
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "IsAlive",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_IsAlive;

        /// <summary>
        /// 返回实体是否已死亡
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "IsDead",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_IsDead;

        /// <summary>
        /// 返回实体是否可以行动
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "CanAct",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_CanAct;

        /// <summary>
        /// 返回实体是否为玩家
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "IsPlayer",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_IsPlayer;

        /// <summary>
        /// 返回实体是否为怪物
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "IsMob",
            typeof(GetBoolFromIdDelegate), typeof(GetBoolFromIdPublicDelegate))]
        public IntPtr Entity_IsMob;

        /// <summary>
        /// 返回实体附加效果的数量
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectCount",
            typeof(GetIntFromIdDelegate), typeof(GetIntFromIdPublicDelegate))]
        public IntPtr Entity_GetEffectCount;

        /// <summary>
        /// 返回实体附加效果的 ID
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectId",
            typeof(GetStringFromIdIntDelegate), typeof(GetStringFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectId;

        /// <summary>
        /// 返回实体附加效果名
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectName",
            typeof(GetStringFromIdIntDelegate), typeof(GetStringFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectName;

        /// <summary>
        /// 返回实体附加效果类别对应 <c>EffectCategory</c> 的数值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectCategory",
            typeof(GetIntFromIdIntDelegate), typeof(GetIntFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectCategory;

        /// <summary>
        /// 返回实体附加效果的剩余持续时间
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectRemainingDuration",
            typeof(GetIntFromIdIntDelegate), typeof(GetIntFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectRemainingDuration;

        /// <summary>
        /// 返回实体附加效果的当前堆叠数
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectCurrentStacks",
            typeof(GetIntFromIdIntDelegate), typeof(GetIntFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectCurrentStacks;

        /// <summary>
        /// 返回实体附加效果的最大堆叠数
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        [HostApi("NativePluginKit.Features.Entities", "Entity", "GetEffectMaxStacks",
            typeof(GetIntFromIdIntDelegate), typeof(GetIntFromIdIntPublicDelegate))]
        public IntPtr Entity_GetEffectMaxStacks;
    }

    /// <summary>
    /// 插件导出的初始化函数签名
    /// 插件必须导出一个名为 <c>OnInit</c> 的函数，参数为指向 <see cref="HostApiTable"/> 的指针
    /// </summary>
    /// <param name="hostApiTable">指向宿主 API 函数表的指针</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PluginInitDelegate(IntPtr hostApiTable);
}
