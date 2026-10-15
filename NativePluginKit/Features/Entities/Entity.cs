// SPDX-FileCopyrightText: 2026 GoWelkin Network
// SPDX-License-Identifier: BSD-3-Clause

using NativePluginKit.Features.Wrappers;
using NativePluginKit.Loader.Plugins.NativeInterop;

namespace NativePluginKit.Features.Entities
{
    /// <summary>
    /// 实体数据查询
    /// </summary>
    public static unsafe class Entity
    {
        private static readonly NativeCall Host = NativeCalls.Get("__host__:Entities");

        /// <summary>
        /// 返回当前场景中的实体数量
        /// </summary>
        public static int GetEntityCount()
            => Host.Invoke<int>(nameof(GetEntityCount));

        /// <summary>
        /// 读取指定索引实体的 ID 到 16 字节缓冲区，低 64 位在前，高 64 位在后
        /// </summary>
        /// <param name="index">实体索引</param>
        /// <param name="out16Bytes">输出缓冲区指针至少需要 16 字节</param>
        /// <returns>成功返回 1, 否则返回 0</returns>
        public static int GetEntityIdAt(int index, IntPtr out16Bytes)
            => Host.Invoke<int>(nameof(GetEntityIdAt), index, out16Bytes);

        /// <summary>
        /// 判断实体是否仍然存在
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static bool Contains(ulong low, ulong high)
            => Host.Invoke<bool>(nameof(Contains), low, high);

        /// <summary>
        /// 返回实体内部名称
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static string GetName(ulong low, ulong high)
            => Host.Invoke<string>(nameof(GetName), low, high);

        /// <summary>
        /// 返回实体显示名称
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static string GetDisplayName(ulong low, ulong high)
            => Host.Invoke<string>(nameof(GetDisplayName), low, high);

        /// <summary>
        /// 返回实体图片路径
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static string GetImagePath(ulong low, ulong high)
            => Host.Invoke<string>(nameof(GetImagePath), low, high);

        /// <summary>
        /// 返回实体最大生命值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static int GetMaxHP(ulong low, ulong high)
            => Host.Invoke<int>(nameof(GetMaxHP), low, high);

        /// <summary>
        /// 返回实体当前生命值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static int GetCurrentHP(ulong low, ulong high)
            => Host.Invoke<int>(nameof(GetCurrentHP), low, high);

        /// <summary>
        /// 返回实体攻击力
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static int GetAttackPower(ulong low, ulong high)
            => Host.Invoke<int>(nameof(GetAttackPower), low, high);

        /// <summary>
        /// 返回实体闪避值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static int GetDodgeChance(ulong low, ulong high)
            => Host.Invoke<int>(nameof(GetDodgeChance), low, high);

        /// <summary>
        /// 返回实体护盾值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static int GetShieldValue(ulong low, ulong high)
            => Host.Invoke<int>(nameof(GetShieldValue), low, high);

        /// <summary>
        /// 返回实体护甲值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static int GetArmorValue(ulong low, ulong high)
            => Host.Invoke<int>(nameof(GetArmorValue), low, high);

        /// <summary>
        /// 返回实体当前费用
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static int GetCurrentCost(ulong low, ulong high)
            => Host.Invoke<int>(nameof(GetCurrentCost), low, high);

        /// <summary>
        /// 返回实体最大费用
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static int GetMaxCost(ulong low, ulong high)
            => Host.Invoke<int>(nameof(GetMaxCost), low, high);

        /// <summary>
        /// 返回实体是否存活
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static bool IsAlive(ulong low, ulong high)
            => Host.Invoke<bool>(nameof(IsAlive), low, high);

        /// <summary>
        /// 返回实体是否已死亡
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static bool IsDead(ulong low, ulong high)
            => Host.Invoke<bool>(nameof(IsDead), low, high);

        /// <summary>
        /// 返回实体是否可以行动
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static bool CanAct(ulong low, ulong high)
            => Host.Invoke<bool>(nameof(CanAct), low, high);

        /// <summary>
        /// 返回实体是否为玩家
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static bool IsPlayer(ulong low, ulong high)
            => Host.Invoke<bool>(nameof(IsPlayer), low, high);

        /// <summary>
        /// 返回实体是否为怪物
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static bool IsMob(ulong low, ulong high)
            => Host.Invoke<bool>(nameof(IsMob), low, high);

        /// <summary>
        /// 返回实体附加效果的数量
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        public static int GetEffectCount(ulong low, ulong high)
            => Host.Invoke<int>(nameof(GetEffectCount), low, high);

        /// <summary>
        /// 返回实体附加效果的 ID
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        public static string GetEffectId(ulong low, ulong high, int index)
            => Host.Invoke<string>(nameof(GetEffectId), low, high, index);

        /// <summary>
        /// 返回实体附加效果名称
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        public static string GetEffectName(ulong low, ulong high, int index)
            => Host.Invoke<string>(nameof(GetEffectName), low, high, index);

        /// <summary>
        /// 返回实体附加效果类别对应 <c>EffectCategory</c> 的数值
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        public static int GetEffectCategory(ulong low, ulong high, int index)
            => Host.Invoke<int>(nameof(GetEffectCategory), low, high, index);

        /// <summary>
        /// 返回实体附加效果的剩余持续时间
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        public static int GetEffectRemainingDuration(ulong low, ulong high, int index)
            => Host.Invoke<int>(nameof(GetEffectRemainingDuration), low, high, index);

        /// <summary>
        /// 返回实体附加效果的当前堆叠数
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        public static int GetEffectCurrentStacks(ulong low, ulong high, int index)
            => Host.Invoke<int>(nameof(GetEffectCurrentStacks), low, high, index);

        /// <summary>
        /// 返回实体附加效果的最大堆叠数
        /// </summary>
        /// <param name="low">Guid 低 64 位</param>
        /// <param name="high">Guid 高 64 位</param>
        /// <param name="index">效果索引</param>
        public static int GetEffectMaxStacks(ulong low, ulong high, int index)
            => Host.Invoke<int>(nameof(GetEffectMaxStacks), low, high, index);

        /// <summary>
        /// 读取指定索引处的实体 Id, 越界时返回默认值
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
