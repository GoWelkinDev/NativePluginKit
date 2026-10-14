// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Events.Handlers;
using System.Runtime.InteropServices;

namespace NativePluginKit.Loader.Constants
{
    /// <summary>
    /// 定义宿主向插件提供的 API 契约;
    /// 由 HostApiBridgeBuilder 转换为 HostApiTable
    /// </summary>
    /// <remarks>
    /// 默认实现的方法，宿主可以按需覆盖;
    /// 可选方法未被覆盖时，默认返回零值或执行空操作
    /// </remarks>
    public interface IHostApi
    {
        /// <summary>
        /// 释放由宿主分配的非托管 UTF-8 字符串
        /// </summary>
        void FreeString(IntPtr ptr) => Marshal.FreeCoTaskMem(ptr);

        /// <summary>
        /// 输出普通日志
        /// </summary>
        void PrintLog(IntPtr messagePtr) { }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        void PrintWarning(IntPtr messagePtr) { }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        void PrintError(IntPtr messagePtr) { }

        /// <summary>
        /// 输出调试日志
        /// </summary>
        void LogDebug(IntPtr messagePtr) { }

        /// <summary>
        /// 返回当前场景资源路径
        /// </summary>
        IntPtr GetCurrentScenePath() => IntPtr.Zero;

        /// <summary>
        /// 返回插件目录路径
        /// </summary>
        IntPtr GetPluginsDirectory() => IntPtr.Zero;

        /// <summary>
        /// 返回自宿主启动以来经过的秒数
        /// </summary>
        double GetTimeSinceStartup() => 0;

        /// <summary>
        /// 注册事件回调
        /// </summary>
        bool RegisterEvent(int eventType, IntPtr callback, IntPtr userData) => EventDispatcher.Register(eventType, callback, userData);

        /// <summary>
        /// 取消事件回调注册
        /// </summary>
        bool UnregisterEvent(int eventType, IntPtr callback, IntPtr userData) => EventDispatcher.Unregister(eventType, callback, userData);

        /// <summary>
        /// 返回当前剧情时间线名称
        /// </summary>
        IntPtr GetCurrentTimelineName() => IntPtr.Zero;

        /// <summary>
        /// 返回当前是否处于战斗状态
        /// </summary>
        int IsBattle() => 0;

        /// <summary>
        /// 返回卡牌总数
        /// </summary>
        int CardGetCount() => 0;

        /// <summary>
        /// 根据索引获取卡牌名称
        /// </summary>
        IntPtr CardGetNameAt(int index) => IntPtr.Zero;

        /// <summary>
        /// 判断指定名称的卡牌是否存在
        /// </summary>
        int CardContains(IntPtr namePtr) => 0;

        /// <summary>
        /// 返回卡牌的显示名称
        /// </summary>
        IntPtr CardGetDisplayName(IntPtr namePtr) => IntPtr.Zero;

        /// <summary>
        /// 返回卡牌描述
        /// </summary>
        IntPtr CardGetDescription(IntPtr namePtr) => IntPtr.Zero;

        /// <summary>
        /// 返回卡牌图片资源名称
        /// </summary>
        IntPtr CardGetImageName(IntPtr namePtr) => IntPtr.Zero;

        /// <summary>
        /// 返回卡牌费用
        /// </summary>
        int CardGetCost(IntPtr namePtr) => 0;

        /// <summary>
        /// 返回卡牌基础攻击力
        /// </summary>
        int CardGetBaseAttack(IntPtr namePtr) => 0;

        /// <summary>
        /// 返回卡牌最大堆叠数
        /// </summary>
        int CardGetMaxStack(IntPtr namePtr) => 0;

        /// <summary>
        /// 返回卡牌权重
        /// </summary>
        int CardGetWeight(IntPtr namePtr) => 0;

        /// <summary>
        /// 返回卡牌攻击类型
        /// </summary>
        int CardGetAttackType(IntPtr namePtr) => 0;

        /// <summary>
        /// 返回卡牌类型
        /// </summary>
        int CardGetCardType(IntPtr namePtr) => 0;

        /// <summary>
        /// 返回卡牌附加效果的数量
        /// </summary>
        int CardGetAppliedEffectCount(IntPtr namePtr) => 0;

        /// <summary>
        /// 返回卡牌附加效果的 ID
        /// </summary>
        IntPtr CardGetAppliedEffectId(IntPtr namePtr, int index) => IntPtr.Zero;

        /// <summary>
        /// 返回卡牌附加效果名称
        /// </summary>
        IntPtr CardGetAppliedEffectName(IntPtr namePtr, int index) => IntPtr.Zero;

        /// <summary>
        /// 返回卡牌附加效果类别
        /// </summary>
        int CardGetAppliedEffectCategory(IntPtr namePtr, int index) => 0;

        /// <summary>
        /// 返回卡牌附加效果持续时间
        /// </summary>
        int CardGetAppliedEffectDuration(IntPtr namePtr, int index) => 0;

        /// <summary>
        /// 返回卡牌附加效果是否可堆叠
        /// </summary>
        int CardGetAppliedEffectIsStackable(IntPtr namePtr, int index) => 0;

        /// <summary>
        /// 返回卡牌附加效果最大堆叠数
        /// </summary>
        int CardGetAppliedEffectMaxStacks(IntPtr namePtr, int index) => 0;

        /// <summary>
        /// 返回卡牌附加效果参数 1
        /// </summary>
        float CardGetAppliedEffectParam1(IntPtr namePtr, int index) => 0;

        /// <summary>
        /// 返回卡牌附加效果参数 2
        /// </summary>
        float CardGetAppliedEffectParam2(IntPtr namePtr, int index) => 0;

        /// <summary>
        /// 返回当前场景中的实体数量
        /// </summary>
        int EntityGetCount() => 0;

        /// <summary>
        /// 读取指定索引实体的 ID 到 16 字节缓冲区
        /// </summary>
        int EntityGetIdAt(int index, IntPtr out16Bytes) => 0;

        /// <summary>
        /// 判断实体是否仍然存在
        /// </summary>
        int EntityContains(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体内部名称
        /// </summary>
        IntPtr EntityGetName(ulong low, ulong high) => IntPtr.Zero;

        /// <summary>
        /// 返回实体显示名称
        /// </summary>
        IntPtr EntityGetDisplayName(ulong low, ulong high) => IntPtr.Zero;

        /// <summary>
        /// 返回实体图片路径
        /// </summary>
        IntPtr EntityGetImagePath(ulong low, ulong high) => IntPtr.Zero;

        /// <summary>
        /// 返回实体最大生命值
        /// </summary>
        int EntityGetMaxHP(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体当前生命值
        /// </summary>
        int EntityGetCurrentHP(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体攻击力
        /// </summary>
        int EntityGetAttackPower(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体闪避值
        /// </summary>
        int EntityGetDodgeChance(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体护盾值
        /// </summary>
        int EntityGetShieldValue(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体护甲值
        /// </summary>
        int EntityGetArmorValue(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体当前费用
        /// </summary>
        int EntityGetCurrentCost(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体最大费用
        /// </summary>
        int EntityGetMaxCost(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体是否存活
        /// </summary>
        int EntityIsAlive(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体是否已死亡
        /// </summary>
        int EntityIsDead(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体是否可以行动
        /// </summary>
        int EntityCanAct(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体是否为玩家
        /// </summary>
        int EntityIsPlayer(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体是否为怪物
        /// </summary>
        int EntityIsMob(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体附加效果的数量
        /// </summary>
        int EntityGetEffectCount(ulong low, ulong high) => 0;

        /// <summary>
        /// 返回实体附加效果的 ID
        /// </summary>
        IntPtr EntityGetEffectId(ulong low, ulong high, int index) => IntPtr.Zero;

        /// <summary>
        /// 返回实体附加效果名称
        /// </summary>
        IntPtr EntityGetEffectName(ulong low, ulong high, int index) => IntPtr.Zero;

        /// <summary>
        /// 返回实体附加效果类别
        /// </summary>
        int EntityGetEffectCategory(ulong low, ulong high, int index) => 0;

        /// <summary>
        /// 返回实体附加效果的剩余持续时间
        /// </summary>
        int EntityGetEffectRemainingDuration(ulong low, ulong high, int index) => 0;

        /// <summary>
        /// 返回实体附加效果的当前堆叠数
        /// </summary>
        int EntityGetEffectCurrentStacks(ulong low, ulong high, int index) => 0;

        /// <summary>
        /// 返回实体附加效果的最大堆叠数
        /// </summary>
        int EntityGetEffectMaxStacks(ulong low, ulong high, int index) => 0;
    }
}
