namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 用于构建 <see cref="HostApiTable"/> 的静态工厂类
    /// 将托管委托转换为非托管函数指针
    /// </summary>
    public static class HostApiBridgeBuilder
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

        /// <summary>
        /// 创建填充了函数指针的 <see cref="HostApiTable"/> 实例
        /// 所有参数由宿主通过 <c>[UnmanagedCallersOnly]</c> 方法取函数指针后传入
        /// </summary>
        /// <returns>包含非托管函数指针的API表结构体</returns>
        public static HostApiTable Create(
            // 基础设施
            IntPtr freeString,
            // 场景
            IntPtr transition,
            IntPtr getCurrentScenePath,
            // 日志
            IntPtr printLog,
            IntPtr printWarning,
            IntPtr printError,
            IntPtr logDebug,
            // 配置
            IntPtr getCharacterImagePath,
            IntPtr getExecutableDirectory,
            IntPtr getPluginsDirectory,
            // 计时
            IntPtr getTimeSinceStartup,
            // 事件
            IntPtr registerEvent,
            IntPtr unregisterEvent,
            // 剧情
            IntPtr getCurrentTimelineName,
            IntPtr isBattle,
            IntPtr startTimeline,
            IntPtr endTimeline,
            IntPtr sendSignal,
            // 卡牌基础
            IntPtr cardGetCount,
            IntPtr cardGetNameAt,
            IntPtr cardContains,
            IntPtr cardGetDisplayName,
            IntPtr cardGetDescription,
            IntPtr cardGetImageName,
            IntPtr cardGetCost,
            IntPtr cardGetBaseAttack,
            IntPtr cardGetMaxStack,
            IntPtr cardGetWeight,
            IntPtr cardGetAttackType,
            IntPtr cardGetCardType,
            // 卡牌效果
            IntPtr cardGetAppliedEffectCount,
            IntPtr cardGetAppliedEffectId,
            IntPtr cardGetAppliedEffectName,
            IntPtr cardGetAppliedEffectCategory,
            IntPtr cardGetAppliedEffectDuration,
            IntPtr cardGetAppliedEffectIsStackable,
            IntPtr cardGetAppliedEffectMaxStacks,
            IntPtr cardGetAppliedEffectParam1,
            IntPtr cardGetAppliedEffectParam2,
            // 实体枚举
            IntPtr entityGetCount,
            IntPtr entityGetIdAt,
            IntPtr entityContains,
            // 实体基础信息
            IntPtr entityGetName,
            IntPtr entityGetDisplayName,
            IntPtr entityGetImagePath,
            // 实体数值
            IntPtr entityGetMaxHP,
            IntPtr entityGetCurrentHP,
            IntPtr entityGetAttackPower,
            IntPtr entityGetDodgeChance,
            IntPtr entityGetShieldValue,
            IntPtr entityGetArmorValue,
            IntPtr entityGetCurrentCost,
            IntPtr entityGetMaxCost,
            // 实体状态
            IntPtr entityIsAlive,
            IntPtr entityIsDead,
            IntPtr entityCanAct,
            IntPtr entityIsPlayer,
            IntPtr entityIsMob,
            // 实体效果
            IntPtr entityGetEffectCount,
            IntPtr entityGetEffectId,
            IntPtr entityGetEffectName,
            IntPtr entityGetEffectCategory,
            IntPtr entityGetEffectRemainingDuration,
            IntPtr entityGetEffectCurrentStacks,
            IntPtr entityGetEffectMaxStacks)
        {
            return new HostApiTable
            {
                FreeString = freeString,

                TransitionToScene = transition,
                Scene_GetCurrentScenePath = getCurrentScenePath,

                PrintLog = printLog,
                PrintWarning = printWarning,
                PrintError = printError,
                LogDebug = logDebug,

                Config_GetCharacterImagePath = getCharacterImagePath,
                Config_GetExecutableDirectory = getExecutableDirectory,
                Config_GetPluginsDirectory = getPluginsDirectory,

                Timing_GetTimeSinceStartup = getTimeSinceStartup,

                RegisterEventCallback = registerEvent,
                UnregisterEventCallback = unregisterEvent,

                Story_GetCurrentTimelineName = getCurrentTimelineName,
                Story_IsBattle = isBattle,
                Story_StartTimeline = startTimeline,
                Story_EndTimeline = endTimeline,
                Story_SendSignal = sendSignal,

                Card_GetCardCount = cardGetCount,
                Card_GetCardNameAt = cardGetNameAt,
                Card_Contains = cardContains,
                Card_GetDisplayName = cardGetDisplayName,
                Card_GetDescription = cardGetDescription,
                Card_GetImageName = cardGetImageName,
                Card_GetCost = cardGetCost,
                Card_GetBaseAttack = cardGetBaseAttack,
                Card_GetMaxStack = cardGetMaxStack,
                Card_GetWeight = cardGetWeight,
                Card_GetAttackType = cardGetAttackType,
                Card_GetCardType = cardGetCardType,

                Card_GetAppliedEffectCount = cardGetAppliedEffectCount,
                Card_GetAppliedEffectId = cardGetAppliedEffectId,
                Card_GetAppliedEffectName = cardGetAppliedEffectName,
                Card_GetAppliedEffectCategory = cardGetAppliedEffectCategory,
                Card_GetAppliedEffectDuration = cardGetAppliedEffectDuration,
                Card_GetAppliedEffectIsStackable = cardGetAppliedEffectIsStackable,
                Card_GetAppliedEffectMaxStacks = cardGetAppliedEffectMaxStacks,
                Card_GetAppliedEffectParam1 = cardGetAppliedEffectParam1,
                Card_GetAppliedEffectParam2 = cardGetAppliedEffectParam2,

                Entity_GetCount = entityGetCount,
                Entity_GetIdAt = entityGetIdAt,
                Entity_Contains = entityContains,

                Entity_GetName = entityGetName,
                Entity_GetDisplayName = entityGetDisplayName,
                Entity_GetImagePath = entityGetImagePath,

                Entity_GetMaxHP = entityGetMaxHP,
                Entity_GetCurrentHP = entityGetCurrentHP,
                Entity_GetAttackPower = entityGetAttackPower,
                Entity_GetDodgeChance = entityGetDodgeChance,
                Entity_GetShieldValue = entityGetShieldValue,
                Entity_GetArmorValue = entityGetArmorValue,
                Entity_GetCurrentCost = entityGetCurrentCost,
                Entity_GetMaxCost = entityGetMaxCost,

                Entity_IsAlive = entityIsAlive,
                Entity_IsDead = entityIsDead,
                Entity_CanAct = entityCanAct,
                Entity_IsPlayer = entityIsPlayer,
                Entity_IsMob = entityIsMob,

                Entity_GetEffectCount = entityGetEffectCount,
                Entity_GetEffectId = entityGetEffectId,
                Entity_GetEffectName = entityGetEffectName,
                Entity_GetEffectCategory = entityGetEffectCategory,
                Entity_GetEffectRemainingDuration = entityGetEffectRemainingDuration,
                Entity_GetEffectCurrentStacks = entityGetEffectCurrentStacks,
                Entity_GetEffectMaxStacks = entityGetEffectMaxStacks,
            };
        }
    }
}
