using System.Runtime.InteropServices;

namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 用于构建 <see cref="HostApiTable"/> 的静态工厂类
    /// 将托管委托转换为非托管函数指针
    /// </summary>
    public static unsafe class HostApiBridgeBuilder
    {
        // ====== 基础设施 ======
        public delegate void FreeStringDelegate(IntPtr ptr);

        // ====== 场景 ======
        public delegate void TransitionToSceneDelegate(IntPtr scenePathPtr, bool fadeOut);
        public delegate void TransitionToScenePublicDelegate(string scenePath, bool fadeOut);

        public delegate IntPtr GetStringDelegate();
        public delegate string GetStringPublicDelegate();

        public delegate IntPtr GetStringFromStringDelegate(IntPtr inputPtr);
        public delegate string GetStringFromStringPublicDelegate(string input);

        // ====== 日志 ======
        public delegate void PrintLogDelegate(IntPtr messagePtr);
        public delegate void PrintLogPublicDelegate(string message);

        // ====== 计时 ======
        public delegate double GetDoubleDelegate();

        // ====== 事件 ======
        public delegate bool RegisterEventDelegate(int eventType, IntPtr callback, IntPtr userData);
        public delegate bool UnregisterEventDelegate(int eventType, IntPtr callback, IntPtr userData);

        // ====== 剧情 ======
        public delegate int GetBoolDelegate();
        public delegate bool GetBoolPublicDelegate();

        public delegate void StartTimelineDelegate(IntPtr namePtr, bool battle);
        public delegate void StartTimelinePublicDelegate(string name, bool battle);

        public delegate void EndTimelineDelegate(bool skipEnding);
        public delegate void EndTimelinePublicDelegate(bool skipEnding);

        // 卡牌：int -> string
        public delegate IntPtr GetStringFromIntDelegate(int index);
        public delegate string GetStringFromIntPublicDelegate(int index);

        // 卡牌：int 无参
        public delegate int GetIntDelegate();
        public delegate int GetIntPublicDelegate();

        // 卡牌：string -> int / bool
        public delegate int GetIntFromStringDelegate(IntPtr namePtr);
        public delegate int GetIntFromStringPublicDelegate(string name);

        public delegate int GetBoolFromStringDelegate(IntPtr namePtr);
        public delegate bool GetBoolFromStringPublicDelegate(string name);

        // 卡牌：string + int -> string / int / bool / float
        public delegate IntPtr GetStringFromStringIntDelegate(IntPtr namePtr, int index);
        public delegate string GetStringFromStringIntPublicDelegate(string name, int index);

        public delegate int GetIntFromStringIntDelegate(IntPtr namePtr, int index);
        public delegate int GetIntFromStringIntPublicDelegate(string name, int index);

        public delegate int GetBoolFromStringIntDelegate(IntPtr namePtr, int index);
        public delegate bool GetBoolFromStringIntPublicDelegate(string name, int index);

        public delegate float GetFloatFromStringIntDelegate(IntPtr namePtr, int index);
        public delegate float GetFloatFromStringIntPublicDelegate(string name, int index);

        // 实体：(ulong, ulong) = ObjectId parts
        public delegate IntPtr GetStringFromIdDelegate(ulong low, ulong high);
        public delegate string GetStringFromIdPublicDelegate(ulong low, ulong high);

        public delegate int GetIntFromIdDelegate(ulong low, ulong high);
        public delegate int GetIntFromIdPublicDelegate(ulong low, ulong high);

        public delegate int GetBoolFromIdDelegate(ulong low, ulong high);
        public delegate bool GetBoolFromIdPublicDelegate(ulong low, ulong high);

        public delegate IntPtr GetStringFromIdIntDelegate(ulong low, ulong high, int index);
        public delegate string GetStringFromIdIntPublicDelegate(ulong low, ulong high, int index);

        public delegate int GetIntFromIdIntDelegate(ulong low, ulong high, int index);
        public delegate int GetIntFromIdIntPublicDelegate(ulong low, ulong high, int index);

        // 实体：id 写入 16 字节缓冲区
        public delegate int GetEntityIdAtDelegate(int index, IntPtr out16Bytes);
        public delegate int GetEntityIdAtPublicDelegate(int index, IntPtr out16Bytes);

        // ====== 委托强引用池 ======

        private static readonly List<Delegate> _keepAlive = new();

        private static IntPtr Ptr(Delegate d)
        {
            _keepAlive.Add(d);
            return Marshal.GetFunctionPointerForDelegate(d);
        }

        /// <summary>
        /// 创建填充了函数指针的 <see cref="HostApiTable"/> 实例
        /// </summary>
        /// <returns>包含非托管函数指针的API表结构体</returns>
        public static HostApiTable Create(
            // 基础设施
            FreeStringDelegate freeString,
            // 场景
            TransitionToSceneDelegate transition,
            GetStringDelegate getCurrentScenePath,
            // 日志
            PrintLogDelegate printLog,
            PrintLogDelegate printWarning,
            PrintLogDelegate printError,
            PrintLogDelegate logDebug,
            // 配置
            GetStringFromStringDelegate getCharacterImagePath,
            GetStringDelegate getExecutableDirectory,
            GetStringDelegate getPluginsDirectory,
            // 计时
            GetDoubleDelegate getTimeSinceStartup,
            // 事件
            RegisterEventDelegate registerEvent,
            UnregisterEventDelegate unregisterEvent,
            // 剧情
            GetStringDelegate getCurrentTimelineName,
            GetBoolDelegate isBattle,
            StartTimelineDelegate startTimeline,
            EndTimelineDelegate endTimeline,
            PrintLogDelegate sendSignal,
            // 卡牌基础
            GetIntDelegate cardGetCount,
            GetStringFromIntDelegate cardGetNameAt,
            GetBoolFromStringDelegate cardContains,
            GetStringFromStringDelegate cardGetDisplayName,
            GetStringFromStringDelegate cardGetDescription,
            GetStringFromStringDelegate cardGetImageName,
            GetIntFromStringDelegate cardGetCost,
            GetIntFromStringDelegate cardGetBaseAttack,
            GetIntFromStringDelegate cardGetMaxStack,
            GetIntFromStringDelegate cardGetWeight,
            GetIntFromStringDelegate cardGetAttackType,
            GetIntFromStringDelegate cardGetCardType,
            // 卡牌效果
            GetIntFromStringDelegate cardGetAppliedEffectCount,
            GetStringFromStringIntDelegate cardGetAppliedEffectId,
            GetStringFromStringIntDelegate cardGetAppliedEffectName,
            GetIntFromStringIntDelegate cardGetAppliedEffectCategory,
            GetIntFromStringIntDelegate cardGetAppliedEffectDuration,
            GetBoolFromStringIntDelegate cardGetAppliedEffectIsStackable,
            GetIntFromStringIntDelegate cardGetAppliedEffectMaxStacks,
            GetFloatFromStringIntDelegate cardGetAppliedEffectParam1,
            GetFloatFromStringIntDelegate cardGetAppliedEffectParam2,
            // 实体枚举
            GetIntDelegate entityGetCount,
            GetEntityIdAtDelegate entityGetIdAt,
            GetBoolFromIdDelegate entityContains,
            // 实体基础信息
            GetStringFromIdDelegate entityGetName,
            GetStringFromIdDelegate entityGetDisplayName,
            GetStringFromIdDelegate entityGetImagePath,
            // 实体数值
            GetIntFromIdDelegate entityGetMaxHP,
            GetIntFromIdDelegate entityGetCurrentHP,
            GetIntFromIdDelegate entityGetAttackPower,
            GetIntFromIdDelegate entityGetDodgeChance,
            GetIntFromIdDelegate entityGetShieldValue,
            GetIntFromIdDelegate entityGetArmorValue,
            GetIntFromIdDelegate entityGetCurrentCost,
            GetIntFromIdDelegate entityGetMaxCost,
            // 实体状态
            GetBoolFromIdDelegate entityIsAlive,
            GetBoolFromIdDelegate entityIsDead,
            GetBoolFromIdDelegate entityCanAct,
            GetBoolFromIdDelegate entityIsPlayer,
            GetBoolFromIdDelegate entityIsMob,
            // 实体效果
            GetIntFromIdDelegate entityGetEffectCount,
            GetStringFromIdIntDelegate entityGetEffectId,
            GetStringFromIdIntDelegate entityGetEffectName,
            GetIntFromIdIntDelegate entityGetEffectCategory,
            GetIntFromIdIntDelegate entityGetEffectRemainingDuration,
            GetIntFromIdIntDelegate entityGetEffectCurrentStacks,
            GetIntFromIdIntDelegate entityGetEffectMaxStacks)
        {
            return new HostApiTable
            {
                FreeString = Ptr(freeString),

                TransitionToScene = Ptr(transition),
                Scene_GetCurrentScenePath = Ptr(getCurrentScenePath),

                PrintLog = Ptr(printLog),
                PrintWarning = Ptr(printWarning),
                PrintError = Ptr(printError),
                LogDebug = Ptr(logDebug),

                Config_GetCharacterImagePath = Ptr(getCharacterImagePath),
                Config_GetExecutableDirectory = Ptr(getExecutableDirectory),
                Config_GetPluginsDirectory = Ptr(getPluginsDirectory),

                Timing_GetTimeSinceStartup = Ptr(getTimeSinceStartup),

                RegisterEventCallback = Ptr(registerEvent),
                UnregisterEventCallback = Ptr(unregisterEvent),

                Story_GetCurrentTimelineName = Ptr(getCurrentTimelineName),
                Story_IsBattle = Ptr(isBattle),
                Story_StartTimeline = Ptr(startTimeline),
                Story_EndTimeline = Ptr(endTimeline),
                Story_SendSignal = Ptr(sendSignal),

                Card_GetCardCount = Ptr(cardGetCount),
                Card_GetCardNameAt = Ptr(cardGetNameAt),
                Card_Contains = Ptr(cardContains),
                Card_GetDisplayName = Ptr(cardGetDisplayName),
                Card_GetDescription = Ptr(cardGetDescription),
                Card_GetImageName = Ptr(cardGetImageName),
                Card_GetCost = Ptr(cardGetCost),
                Card_GetBaseAttack = Ptr(cardGetBaseAttack),
                Card_GetMaxStack = Ptr(cardGetMaxStack),
                Card_GetWeight = Ptr(cardGetWeight),
                Card_GetAttackType = Ptr(cardGetAttackType),
                Card_GetCardType = Ptr(cardGetCardType),

                Card_GetAppliedEffectCount = Ptr(cardGetAppliedEffectCount),
                Card_GetAppliedEffectId = Ptr(cardGetAppliedEffectId),
                Card_GetAppliedEffectName = Ptr(cardGetAppliedEffectName),
                Card_GetAppliedEffectCategory = Ptr(cardGetAppliedEffectCategory),
                Card_GetAppliedEffectDuration = Ptr(cardGetAppliedEffectDuration),
                Card_GetAppliedEffectIsStackable = Ptr(cardGetAppliedEffectIsStackable),
                Card_GetAppliedEffectMaxStacks = Ptr(cardGetAppliedEffectMaxStacks),
                Card_GetAppliedEffectParam1 = Ptr(cardGetAppliedEffectParam1),
                Card_GetAppliedEffectParam2 = Ptr(cardGetAppliedEffectParam2),

                Entity_GetCount = Ptr(entityGetCount),
                Entity_GetIdAt = Ptr(entityGetIdAt),
                Entity_Contains = Ptr(entityContains),

                Entity_GetName = Ptr(entityGetName),
                Entity_GetDisplayName = Ptr(entityGetDisplayName),
                Entity_GetImagePath = Ptr(entityGetImagePath),

                Entity_GetMaxHP = Ptr(entityGetMaxHP),
                Entity_GetCurrentHP = Ptr(entityGetCurrentHP),
                Entity_GetAttackPower = Ptr(entityGetAttackPower),
                Entity_GetDodgeChance = Ptr(entityGetDodgeChance),
                Entity_GetShieldValue = Ptr(entityGetShieldValue),
                Entity_GetArmorValue = Ptr(entityGetArmorValue),
                Entity_GetCurrentCost = Ptr(entityGetCurrentCost),
                Entity_GetMaxCost = Ptr(entityGetMaxCost),

                Entity_IsAlive = Ptr(entityIsAlive),
                Entity_IsDead = Ptr(entityIsDead),
                Entity_CanAct = Ptr(entityCanAct),
                Entity_IsPlayer = Ptr(entityIsPlayer),
                Entity_IsMob = Ptr(entityIsMob),

                Entity_GetEffectCount = Ptr(entityGetEffectCount),
                Entity_GetEffectId = Ptr(entityGetEffectId),
                Entity_GetEffectName = Ptr(entityGetEffectName),
                Entity_GetEffectCategory = Ptr(entityGetEffectCategory),
                Entity_GetEffectRemainingDuration = Ptr(entityGetEffectRemainingDuration),
                Entity_GetEffectCurrentStacks = Ptr(entityGetEffectCurrentStacks),
                Entity_GetEffectMaxStacks = Ptr(entityGetEffectMaxStacks),
            };
        }
    }
}
