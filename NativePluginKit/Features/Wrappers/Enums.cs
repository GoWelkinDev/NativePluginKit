namespace NativePluginKit.Features.Wrappers
{
    /// <summary>
    /// 攻击属性，数值与宿主 <c>AttackType</c> 一致
    /// </summary>
    public enum CardAttackType
    {
        Physical = 0,
        Magic = 1,
        Fire = 2,
        Ice = 3,
        Thunder = 4,
        True = 5,
    }

    /// <summary>
    /// 卡牌类型，数值与宿主 <c>CardType</c> 一致
    /// </summary>
    public enum CardCategory
    {
        Skill = 0,
        Environment = 1,
        Special = 2,
    }

    /// <summary>
    /// 状态效果类别，数值与宿主 <c>EffectCategory</c> 一致
    /// </summary>
    public enum EffectCategory
    {
        DamageOverTime = 0,
        Fire = 1,
        Stun = 2,
        StatMod = 3,
        Confuse = 4,
        Instant = 5,
    }
}
