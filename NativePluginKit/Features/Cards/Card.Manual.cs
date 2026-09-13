using NativePluginKit.Features.Wrappers;

namespace NativePluginKit.Features.Cards
{
    /// <summary>
    /// <see cref="Card"/> 的手写扩展部分
    /// 提供强类型枚举返回和更自然的调用形式
    /// </summary>
    public static unsafe partial class Card
    {
        /// <summary>
        /// 卡牌攻击属性（强类型）
        /// </summary>
        public static CardAttackType GetAttackTypeEnum(string name)
            => (CardAttackType)GetAttackType(name);

        /// <summary>
        /// 卡牌类型（强类型）
        /// </summary>
        public static CardCategory GetCardTypeEnum(string name)
            => (CardCategory)GetCardType(name);

        /// <summary>
        /// 卡牌效果的状态类别（强类型）
        /// </summary>
        public static EffectCategory GetAppliedEffectCategoryEnum(string name, int index)
            => (EffectCategory)GetAppliedEffectCategory(name, index);

        /// <summary>
        /// 一次性返回卡牌的所有效果（按顺序）
        /// 每个元素是一个 (EffectId, Name, Category) 元组
        /// </summary>
        public static CardEffectInfo[] GetAppliedEffects(string name)
        {
            int count = GetAppliedEffectCount(name);
            if (count <= 0) return System.Array.Empty<CardEffectInfo>();

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
