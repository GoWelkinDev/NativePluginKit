using System.Runtime.InteropServices;

namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 用于构建 <see cref="HostApiTable"/> 的静态工厂类
    /// 将托管委托转换为非托管函数指针
    /// </summary>
    public static class HostApiBridgeBuilder
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FreeStringDelegate(IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetStringDelegate();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string GetStringPublicDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetStringFromStringDelegate(IntPtr inputPtr);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string GetStringFromStringPublicDelegate(string imageName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int RegisterPluginMethodDelegate(IntPtr pluginName, IntPtr methodName, IntPtr fnPtr);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int RegisterPluginMethodPublicDelegate(string pluginName, string methodName, IntPtr fnPtr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int UnregisterPluginMethodDelegate(IntPtr pluginName, IntPtr methodName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int UnregisterPluginMethodPublicDelegate(string pluginName, string methodName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetPluginMethodDelegate(IntPtr pluginName, IntPtr methodName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetPluginMethodPublicDelegate(string pluginName, string methodName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PrintLogDelegate(IntPtr messagePtr);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PrintLogPublicDelegate(string message);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate double GetDoubleDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool RegisterEventDelegate(int eventType, IntPtr callback, IntPtr userData);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool UnregisterEventDelegate(int eventType, IntPtr callback, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetBoolDelegate();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool GetBoolPublicDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetStringFromIntDelegate(int index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string GetStringFromIntPublicDelegate(int index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetIntDelegate();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetIntPublicDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetIntFromStringDelegate(IntPtr namePtr);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetIntFromStringPublicDelegate(string name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetBoolFromStringDelegate(IntPtr namePtr);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool GetBoolFromStringPublicDelegate(string name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetStringFromStringIntDelegate(IntPtr namePtr, int index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string GetStringFromStringIntPublicDelegate(string name, int index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetIntFromStringIntDelegate(IntPtr namePtr, int index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetIntFromStringIntPublicDelegate(string name, int index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetBoolFromStringIntDelegate(IntPtr namePtr, int index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool GetBoolFromStringIntPublicDelegate(string name, int index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate float GetFloatFromStringIntDelegate(IntPtr namePtr, int index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate float GetFloatFromStringIntPublicDelegate(string name, int index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetStringFromIdDelegate(ulong low, ulong high);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string GetStringFromIdPublicDelegate(ulong low, ulong high);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetIntFromIdDelegate(ulong low, ulong high);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetIntFromIdPublicDelegate(ulong low, ulong high);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetBoolFromIdDelegate(ulong low, ulong high);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool GetBoolFromIdPublicDelegate(ulong low, ulong high);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr GetStringFromIdIntDelegate(ulong low, ulong high, int index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string GetStringFromIdIntPublicDelegate(ulong low, ulong high, int index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetIntFromIdIntDelegate(ulong low, ulong high, int index);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetIntFromIdIntPublicDelegate(ulong low, ulong high, int index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetEntityIdAtDelegate(int index, IntPtr out16Bytes);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetEntityIdAtPublicDelegate(int index, IntPtr out16Bytes);

        private static readonly List<Delegate> _keepAlive = new();

        private static IntPtr Ptr<TDelegate>(TDelegate d) where TDelegate : Delegate
        {
            _keepAlive.Add(d);
            return Marshal.GetFunctionPointerForDelegate(d);
        }

        /// <summary>
        /// 创建填充了函数指针的 <see cref="HostApiTable"/> 实例
        /// 所有参数由宿主通过 <c>[UnmanagedCallersOnly]</c> 方法取函数指针后传入
        /// </summary>
        /// <returns>包含非托管函数指针的API表结构体</returns>
        public static HostApiTable Create(
            FreeStringDelegate freeString,
            RegisterPluginMethodDelegate registerPluginMethod,
            UnregisterPluginMethodDelegate unregisterPluginMethod,
            GetPluginMethodDelegate getPluginMethod,

            GetStringDelegate getCurrentScenePath,

            PrintLogDelegate printLog,
            PrintLogDelegate printWarning,
            FreeStringDelegate printError,
            FreeStringDelegate logDebug,

            GetStringFromStringDelegate getCharacterImagePath,
            GetStringDelegate getPluginsDirectory,

            GetDoubleDelegate getTimeSinceStartup,

            RegisterEventDelegate registerEvent,
            UnregisterEventDelegate unregisterEvent,

            GetStringDelegate getCurrentTimelineName,
            GetBoolDelegate isBattle,

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

            GetIntFromStringDelegate cardGetAppliedEffectCount,
            GetStringFromStringIntDelegate cardGetAppliedEffectId,
            GetStringFromStringIntDelegate cardGetAppliedEffectName,
            GetIntFromStringIntDelegate cardGetAppliedEffectCategory,
            GetIntFromStringIntDelegate cardGetAppliedEffectDuration,
            GetBoolFromStringIntDelegate cardGetAppliedEffectIsStackable,
            GetIntFromStringIntDelegate cardGetAppliedEffectMaxStacks,
            GetFloatFromStringIntDelegate cardGetAppliedEffectParam1,
            GetFloatFromStringIntDelegate cardGetAppliedEffectParam2,

            GetIntDelegate entityGetCount,
            GetEntityIdAtDelegate entityGetIdAt,
            GetBoolFromIdDelegate entityContains,

            GetStringFromIdDelegate entityGetName,
            GetStringFromIdDelegate entityGetDisplayName,
            GetStringFromIdDelegate entityGetImagePath,

            GetIntFromIdDelegate entityGetMaxHP,
            GetIntFromIdDelegate entityGetCurrentHP,
            GetIntFromIdDelegate entityGetAttackPower,
            GetIntFromIdDelegate entityGetDodgeChance,
            GetIntFromIdDelegate entityGetShieldValue,
            GetIntFromIdDelegate entityGetArmorValue,
            GetIntFromIdDelegate entityGetCurrentCost,
            GetIntFromIdDelegate entityGetMaxCost,

            GetBoolFromIdDelegate entityIsAlive,
            GetBoolFromIdDelegate entityIsDead,
            GetBoolFromIdDelegate entityCanAct,
            GetBoolFromIdDelegate entityIsPlayer,
            GetBoolFromIdDelegate entityIsMob,

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
                PluginMethods_Register = Ptr(registerPluginMethod),
                PluginMethods_Unregister = Ptr(unregisterPluginMethod),
                PluginMethods_Get = Ptr(getPluginMethod),

                Scene_GetCurrentScenePath = Ptr(getCurrentScenePath),

                PrintLog = Ptr(printLog),
                PrintWarning = Ptr(printWarning),
                PrintError = Ptr(printError),
                LogDebug = Ptr(logDebug),

                Config_GetCharacterImagePath = Ptr(getCharacterImagePath),
                Config_GetPluginsDirectory = Ptr(getPluginsDirectory),

                Timing_GetTimeSinceStartup = Ptr(getTimeSinceStartup),

                RegisterEventCallback = Ptr(registerEvent),
                UnregisterEventCallback = Ptr(unregisterEvent),

                Story_GetCurrentTimelineName = Ptr(getCurrentTimelineName),
                Story_IsBattle = Ptr(isBattle),

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
