using NativePluginKit.Features.Wrappers;

namespace NativePluginKit.Features.Entities
{
    /// <summary>
    /// <see cref="Entity"/> 的手写扩展部分
    /// 提供基于 <see cref="EntityId"/> 的便捷重载
    /// </summary>
    public static unsafe partial class Entity
    {
        /// <summary>
        /// 读取指定索引处的实体 Id。越界时返回默认值
        /// </summary>
        public static EntityId GetEntityIdAt(int index)
        {
            Span<byte> buf = stackalloc byte[16];
            fixed (byte* p = buf)
            {
                if (GetEntityIdAt(index, (IntPtr)p) == 0)
                    return default;
            }
            return new EntityId(
                BitConverter.ToUInt64(buf[..8]),
                BitConverter.ToUInt64(buf.Slice(8, 8)));
        }

        /// <summary>
        /// 一次性枚举所有活动实体 Id
        /// </summary>
        public static EntityId[] GetAllEntityIds()
        {
            int count = GetEntityCount();
            if (count <= 0) return Array.Empty<EntityId>();

            var result = new EntityId[count];
            Span<byte> buf = stackalloc byte[16];
            for (int i = 0; i < count; i++)
            {
                fixed (byte* p = buf)
                {
                    if (GetEntityIdAt(i, (IntPtr)p) == 0) continue;
                }
                result[i] = new EntityId(
                    BitConverter.ToUInt64(buf[..8]),
                    BitConverter.ToUInt64(buf.Slice(8, 8)));
            }
            return result;
        }

        // ====== 便捷重载 ======

        public static bool Contains(EntityId id) => Contains(id.Low, id.High);

        public static string GetName(EntityId id) => GetName(id.Low, id.High);
        public static string GetDisplayName(EntityId id) => GetDisplayName(id.Low, id.High);
        public static string GetImagePath(EntityId id) => GetImagePath(id.Low, id.High);

        public static int GetMaxHP(EntityId id) => GetMaxHP(id.Low, id.High);
        public static int GetCurrentHP(EntityId id) => GetCurrentHP(id.Low, id.High);
        public static int GetAttackPower(EntityId id) => GetAttackPower(id.Low, id.High);
        public static int GetDodgeChance(EntityId id) => GetDodgeChance(id.Low, id.High);
        public static int GetShieldValue(EntityId id) => GetShieldValue(id.Low, id.High);
        public static int GetArmorValue(EntityId id) => GetArmorValue(id.Low, id.High);
        public static int GetCurrentCost(EntityId id) => GetCurrentCost(id.Low, id.High);
        public static int GetMaxCost(EntityId id) => GetMaxCost(id.Low, id.High);

        public static bool IsAlive(EntityId id) => IsAlive(id.Low, id.High);
        public static bool IsDead(EntityId id) => IsDead(id.Low, id.High);
        public static bool CanAct(EntityId id) => CanAct(id.Low, id.High);
        public static bool IsPlayer(EntityId id) => IsPlayer(id.Low, id.High);
        public static bool IsMob(EntityId id) => IsMob(id.Low, id.High);

        public static int GetEffectCount(EntityId id) => GetEffectCount(id.Low, id.High);

        public static string GetEffectId(EntityId id, int i) => GetEffectId(id.Low, id.High, i);
        public static string GetEffectName(EntityId id, int i) => GetEffectName(id.Low, id.High, i);

        public static EffectCategory GetEffectCategoryEnum(EntityId id, int i)
            => (EffectCategory)GetEffectCategory(id.Low, id.High, i);

        public static int GetEffectRemainingDuration(EntityId id, int i)
            => GetEffectRemainingDuration(id.Low, id.High, i);

        public static int GetEffectCurrentStacks(EntityId id, int i)
            => GetEffectCurrentStacks(id.Low, id.High, i);

        public static int GetEffectMaxStacks(EntityId id, int i)
            => GetEffectMaxStacks(id.Low, id.High, i);

        /// <summary>
        /// 一次性返回实体身上的所有效果快照
        /// </summary>
        public static EntityEffectInfo[] GetAllEffects(EntityId id)
        {
            int count = GetEffectCount(id.Low, id.High);
            if (count <= 0) return Array.Empty<EntityEffectInfo>();

            var result = new EntityEffectInfo[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = new EntityEffectInfo(
                    EffectId: GetEffectId(id.Low, id.High, i),
                    Name: GetEffectName(id.Low, id.High, i),
                    Category: (EffectCategory)GetEffectCategory(id.Low, id.High, i),
                    RemainingDuration: GetEffectRemainingDuration(id.Low, id.High, i),
                    CurrentStacks: GetEffectCurrentStacks(id.Low, id.High, i),
                    MaxStacks: GetEffectMaxStacks(id.Low, id.High, i));
            }
            return result;
        }
    }

    /// <summary>
    /// 实体身上一个状态效果的只读快照
    /// </summary>
    public readonly record struct EntityEffectInfo(
        string EffectId,
        string Name,
        EffectCategory Category,
        int RemainingDuration,
        int CurrentStacks,
        int MaxStacks);
}
