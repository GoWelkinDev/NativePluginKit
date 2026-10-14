// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

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
        /// 从宿主实现的 <see cref="IHostApi"/> 构建 <see cref="HostApiTable"/>
        /// </summary>
        /// <param name="api">宿主实现的接口实例</param>
        /// <returns>包含非托管函数指针的 API 表结构体</returns>
        public static HostApiTable Create(IHostApi api)
        {
            return new HostApiTable
            {
                FreeString = Ptr<FreeStringDelegate>(api.FreeString),
                PluginMethods_Register = Ptr<RegisterPluginMethodDelegate>(static (namePtr, methodPtr, fnPtr) =>
                    PluginMethodRegistry.Register(
                        Marshal.PtrToStringUTF8(namePtr) ?? "",
                        Marshal.PtrToStringUTF8(methodPtr) ?? "",
                        fnPtr) ? 1 : 0),

                PluginMethods_Unregister = Ptr<UnregisterPluginMethodDelegate>(static (namePtr, methodPtr) =>
                    PluginMethodRegistry.Unregister(
                        Marshal.PtrToStringUTF8(namePtr) ?? "",
                        Marshal.PtrToStringUTF8(methodPtr) ?? "") ? 1 : 0),

                PluginMethods_Get = Ptr<GetPluginMethodDelegate>(static (namePtr, methodPtr) =>
                    PluginMethodRegistry.Get(
                        Marshal.PtrToStringUTF8(namePtr) ?? "",
                        Marshal.PtrToStringUTF8(methodPtr) ?? "")),

                Scene_GetCurrentScenePath = Ptr<GetStringDelegate>(api.GetCurrentScenePath),

                PrintLog = Ptr<PrintLogDelegate>(api.PrintLog),
                PrintWarning = Ptr<PrintLogDelegate>(api.PrintWarning),
                PrintError = Ptr<PrintLogDelegate>(api.PrintError),
                LogDebug = Ptr<PrintLogDelegate>(api.LogDebug),

                Config_GetPluginsDirectory = Ptr<GetStringDelegate>(api.GetPluginsDirectory),

                Timing_GetTimeSinceStartup = Ptr<GetDoubleDelegate>(api.GetTimeSinceStartup),

                RegisterEventCallback = Ptr<RegisterEventDelegate>(api.RegisterEvent),
                UnregisterEventCallback = Ptr<UnregisterEventDelegate>(api.UnregisterEvent),

                Story_GetCurrentTimelineName = Ptr<GetStringDelegate>(api.GetCurrentTimelineName),
                Story_IsBattle = Ptr<GetBoolDelegate>(api.IsBattle),

                Card_GetCardCount = Ptr<GetIntDelegate>(api.CardGetCount),
                Card_GetCardNameAt = Ptr<GetStringFromIntDelegate>(api.CardGetNameAt),
                Card_Contains = Ptr<GetBoolFromStringDelegate>(api.CardContains),
                Card_GetDisplayName = Ptr<GetStringFromStringDelegate>(api.CardGetDisplayName),
                Card_GetDescription = Ptr<GetStringFromStringDelegate>(api.CardGetDescription),
                Card_GetImageName = Ptr<GetStringFromStringDelegate>(api.CardGetImageName),
                Card_GetCost = Ptr<GetIntFromStringDelegate>(api.CardGetCost),
                Card_GetBaseAttack = Ptr<GetIntFromStringDelegate>(api.CardGetBaseAttack),
                Card_GetMaxStack = Ptr<GetIntFromStringDelegate>(api.CardGetMaxStack),
                Card_GetWeight = Ptr<GetIntFromStringDelegate>(api.CardGetWeight),
                Card_GetAttackType = Ptr<GetIntFromStringDelegate>(api.CardGetAttackType),
                Card_GetCardType = Ptr<GetIntFromStringDelegate>(api.CardGetCardType),

                Card_GetAppliedEffectCount = Ptr<GetIntFromStringDelegate>(api.CardGetAppliedEffectCount),
                Card_GetAppliedEffectId = Ptr<GetStringFromStringIntDelegate>(api.CardGetAppliedEffectId),
                Card_GetAppliedEffectName = Ptr<GetStringFromStringIntDelegate>(api.CardGetAppliedEffectName),
                Card_GetAppliedEffectCategory = Ptr<GetIntFromStringIntDelegate>(api.CardGetAppliedEffectCategory),
                Card_GetAppliedEffectDuration = Ptr<GetIntFromStringIntDelegate>(api.CardGetAppliedEffectDuration),
                Card_GetAppliedEffectIsStackable = Ptr<GetBoolFromStringIntDelegate>(api.CardGetAppliedEffectIsStackable),
                Card_GetAppliedEffectMaxStacks = Ptr<GetIntFromStringIntDelegate>(api.CardGetAppliedEffectMaxStacks),
                Card_GetAppliedEffectParam1 = Ptr<GetFloatFromStringIntDelegate>(api.CardGetAppliedEffectParam1),
                Card_GetAppliedEffectParam2 = Ptr<GetFloatFromStringIntDelegate>(api.CardGetAppliedEffectParam2),

                Entity_GetCount = Ptr<GetIntDelegate>(api.EntityGetCount),
                Entity_GetIdAt = Ptr<GetEntityIdAtDelegate>(api.EntityGetIdAt),
                Entity_Contains = Ptr<GetBoolFromIdDelegate>(api.EntityContains),

                Entity_GetName = Ptr<GetStringFromIdDelegate>(api.EntityGetName),
                Entity_GetDisplayName = Ptr<GetStringFromIdDelegate>(api.EntityGetDisplayName),
                Entity_GetImagePath = Ptr<GetStringFromIdDelegate>(api.EntityGetImagePath),

                Entity_GetMaxHP = Ptr<GetIntFromIdDelegate>(api.EntityGetMaxHP),
                Entity_GetCurrentHP = Ptr<GetIntFromIdDelegate>(api.EntityGetCurrentHP),
                Entity_GetAttackPower = Ptr<GetIntFromIdDelegate>(api.EntityGetAttackPower),
                Entity_GetDodgeChance = Ptr<GetIntFromIdDelegate>(api.EntityGetDodgeChance),
                Entity_GetShieldValue = Ptr<GetIntFromIdDelegate>(api.EntityGetShieldValue),
                Entity_GetArmorValue = Ptr<GetIntFromIdDelegate>(api.EntityGetArmorValue),
                Entity_GetCurrentCost = Ptr<GetIntFromIdDelegate>(api.EntityGetCurrentCost),
                Entity_GetMaxCost = Ptr<GetIntFromIdDelegate>(api.EntityGetMaxCost),

                Entity_IsAlive = Ptr<GetBoolFromIdDelegate>(api.EntityIsAlive),
                Entity_IsDead = Ptr<GetBoolFromIdDelegate>(api.EntityIsDead),
                Entity_CanAct = Ptr<GetBoolFromIdDelegate>(api.EntityCanAct),
                Entity_IsPlayer = Ptr<GetBoolFromIdDelegate>(api.EntityIsPlayer),
                Entity_IsMob = Ptr<GetBoolFromIdDelegate>(api.EntityIsMob),

                Entity_GetEffectCount = Ptr<GetIntFromIdDelegate>(api.EntityGetEffectCount),
                Entity_GetEffectId = Ptr<GetStringFromIdIntDelegate>(api.EntityGetEffectId),
                Entity_GetEffectName = Ptr<GetStringFromIdIntDelegate>(api.EntityGetEffectName),
                Entity_GetEffectCategory = Ptr<GetIntFromIdIntDelegate>(api.EntityGetEffectCategory),
                Entity_GetEffectRemainingDuration = Ptr<GetIntFromIdIntDelegate>(api.EntityGetEffectRemainingDuration),
                Entity_GetEffectCurrentStacks = Ptr<GetIntFromIdIntDelegate>(api.EntityGetEffectCurrentStacks),
                Entity_GetEffectMaxStacks = Ptr<GetIntFromIdIntDelegate>(api.EntityGetEffectMaxStacks),
            };
        }
    }
}
