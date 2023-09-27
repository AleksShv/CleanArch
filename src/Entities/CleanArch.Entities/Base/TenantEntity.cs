namespace CleanArch.Entities.Base;

public abstract class TenantEntity<TId> : Entity<TId>, ITenantEntity
    where TId : IEquatable<TId>
{
    public int TenantId { get; init; }
}
