namespace NativePluginKit.Features.Wrappers
{
    /// <summary>
    /// 跨 ABI 的实体标识（Guid 的低/高 64 位）
    /// 插件通过 <c>Entity.GetEntityIdAt(index)</c> 获取，之后可将其传回任意实体 API
    /// </summary>
    public readonly struct EntityId : IEquatable<EntityId>
    {
        /// <summary>
        /// Guid 的低 64 位（小端序前 8 字节）
        /// </summary>
        public readonly ulong Low;

        /// <summary>
        /// Guid 的高 64 位（小端序后 8 字节）
        /// </summary>
        public readonly ulong High;

        public EntityId(ulong low, ulong high)
        {
            Low = low;
            High = high;
        }

        public bool Equals(EntityId other) => Low == other.Low && High == other.High;
        public override bool Equals(object? obj) => obj is EntityId e && Equals(e);
        public override int GetHashCode() => HashCode.Combine(Low, High);

        public static bool operator ==(EntityId a, EntityId b) => a.Equals(b);
        public static bool operator !=(EntityId a, EntityId b) => !a.Equals(b);

        public override string ToString() => $"{High:X16}{Low:X16}";

        /// <summary>
        /// 是否为默认值（无有效实体）
        /// </summary>
        public bool IsEmpty => Low == 0 && High == 0;
    }
}
