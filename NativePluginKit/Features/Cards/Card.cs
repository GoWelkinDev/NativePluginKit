// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Features.Wrappers;
using NativePluginKit.Loader.Plugins.NativeInterop;

namespace NativePluginKit.Features.Cards
{
    /// <summary>
    /// <see cref="Card"/> 的手写扩展部分
    /// 提供强类型枚举返回和更自然的调用形式
    /// </summary>
    public static class Card
    {
        private static readonly NativeCall Host = NativeCalls.Get("__host__:Cards");

        /// <summary>
        /// 返回卡牌总数
        /// </summary>
        public static int GetCardCount()
            => Host.Invoke<int>(nameof(GetCardCount));

        /// <summary>
        /// 根据索引获取卡牌名称
        /// </summary>
        /// <param name="index">卡牌索引</param>
        public static string GetCardNameAt(int index)
            => Host.Invoke<string>(nameof(GetCardNameAt), index);

        /// <summary>
        /// 判断指定名称的卡牌是否存在
        /// </summary>
        /// <param name="name">卡牌名称</param>
        public static bool Contains(string name)
            => Host.Invoke<bool>(nameof(Contains), name);

        /// <summary>
        /// 返回卡牌的显示名称
        /// </summary>
        /// <param name="imageName">卡牌名称</param>
        public static string GetDisplayName(string imageName)
            => Host.Invoke<string>(nameof(GetDisplayName), imageName);

        /// <summary>
        /// 返回卡牌描述
        /// </summary>
        /// <param name="name">卡牌名称</param>
        public static string GetDescription(string name)
            => Host.Invoke<string>(nameof(GetDescription), name);

        /// <summary>
        /// 返回卡牌图片资源名称
        /// </summary>
        /// <param name="name">卡牌名称</param>
        public static string GetImageName(string name)
            => Host.Invoke<string>(nameof(GetImageName), name);

        /// <summary>
        /// 返回卡牌费用
        /// </summary>
        /// <param name="name">卡牌名称</param>
        public static int GetCost(string name)
            => Host.Invoke<int>(nameof(GetCost), name);

        /// <summary>
        /// 返回卡牌基础攻击力
        /// </summary>
        /// <param name="name">卡牌名称</param>
        public static int GetBaseAttack(string name)
            => Host.Invoke<int>(nameof(GetBaseAttack), name);

        /// <summary>
        /// 返回卡牌最大堆叠数
        /// </summary>
        /// <param name="name">卡牌名称</param>
        public static int GetMaxStack(string name)
            => Host.Invoke<int>(nameof(GetMaxStack), name);

        /// <summary>
        /// 返回卡牌权重
        /// </summary>
        /// <param name="name">卡牌名称</param>
        public static int GetWeight(string name)
            => Host.Invoke<int>(nameof(GetWeight), name);

        /// <summary>
        /// 返回卡牌攻击类型对应 <c>CardAttackType</c> 的数值
        /// </summary>
        /// <param name="name">卡牌名称</param>
        public static int GetAttackType(string name)
            => Host.Invoke<int>(nameof(GetAttackType), name);

        /// <summary>
        /// 返回卡牌类型对应 <c>CardCategory</c> 的数值
        /// </summary>
        /// <param name="name">卡牌名称</param>
        public static int GetCardType(string name)
            => Host.Invoke<int>(nameof(GetCardType), name);

        /// <summary>
        /// 返回卡牌附加效果的数量
        /// </summary>
        /// <param name="name">卡牌名称</param>
        public static int GetAppliedEffectCount(string name)
            => Host.Invoke<int>(nameof(GetAppliedEffectCount), name);

        /// <summary>
        /// 返回卡牌附加效果的 ID
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        public static string GetAppliedEffectId(string name, int index)
            => Host.Invoke<string>(nameof(GetAppliedEffectId), name, index);

        /// <summary>
        /// 返回卡牌附加效果名称
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        public static string GetAppliedEffectName(string name, int index)
            => Host.Invoke<string>(nameof(GetAppliedEffectName), name, index);

        /// <summary>
        /// 返回卡牌附加效果类别对应 <c>EffectCategory</c> 的数值
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        public static int GetAppliedEffectCategory(string name, int index)
            => Host.Invoke<int>(nameof(GetAppliedEffectCategory), name, index);

        /// <summary>
        /// 返回卡牌附加效果的持续时间
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        public static int GetAppliedEffectDuration(string name, int index)
            => Host.Invoke<int>(nameof(GetAppliedEffectDuration), name, index);

        /// <summary>
        /// 返回卡牌附加效果是否可堆叠
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        public static bool GetAppliedEffectIsStackable(string name, int index)
            => Host.Invoke<bool>(nameof(GetAppliedEffectIsStackable), name, index);

        /// <summary>
        /// 返回卡牌附加效果的最大堆叠数
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        public static int GetAppliedEffectMaxStacks(string name, int index)
            => Host.Invoke<int>(nameof(GetAppliedEffectMaxStacks), name, index);

        /// <summary>
        /// 返回卡牌附加效果的参数 1
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        public static float GetAppliedEffectParam1(string name, int index)
            => Host.Invoke<float>(nameof(GetAppliedEffectParam1), name, index);

        /// <summary>
        /// 返回卡牌附加效果的参数 2
        /// </summary>
        /// <param name="name">卡牌名称</param>
        /// <param name="index">效果索引</param>
        public static float GetAppliedEffectParam2(string name, int index)
            => Host.Invoke<float>(nameof(GetAppliedEffectParam2), name, index);

        /// <summary>
        /// 卡牌攻击属性
        /// </summary>
        public static CardAttackType GetAttackTypeEnum(string name)
            => (CardAttackType)GetAttackType(name);

        /// <summary>
        /// 卡牌类型
        /// </summary>
        public static CardCategory GetCardTypeEnum(string name)
            => (CardCategory)GetCardType(name);

        /// <summary>
        /// 卡牌效果的状态类别
        /// </summary>
        public static EffectCategory GetAppliedEffectCategoryEnum(string name, int index)
            => (EffectCategory)GetAppliedEffectCategory(name, index);

        /// <summary>
        /// 一次性按顺序返回卡牌的所有效果
        /// 每个元素是一个 <c>(EffectId, Name, Category)</c> 元组
        /// </summary>
        public static CardEffectInfo[] GetAppliedEffects(string name)
        {
            int count = GetAppliedEffectCount(name);
            if (count <= 0) return Array.Empty<CardEffectInfo>();

            var result = new CardEffectInfo[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = new CardEffectInfo(
                    EffectId: GetAppliedEffectId(name, i),
                    Name: GetAppliedEffectName(name, i),
                    Category: (EffectCategory)GetAppliedEffectCategory(name, i),
                    Duration: GetAppliedEffectDuration(name, i),
                    IsStackable: GetAppliedEffectIsStackable(name, i),
                    MaxStacks: GetAppliedEffectMaxStacks(name, i),
                    Param1: GetAppliedEffectParam1(name, i),
                    Param2: GetAppliedEffectParam2(name, i));
            }
            return result;
        }
    }

    /// <summary>
    /// 卡牌附带效果的只读快照，由 <see cref="Card.GetAppliedEffects(string)"/> 返回
    /// </summary>
    public readonly record struct CardEffectInfo(
        string EffectId,
        string Name,
        EffectCategory Category,
        int Duration,
        bool IsStackable,
        int MaxStacks,
        float Param1,
        float Param2);
}
