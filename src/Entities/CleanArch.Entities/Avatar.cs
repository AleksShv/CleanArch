using CleanArch.Entities.Base;
using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Entities;

public class Avatar : FileEntity<Guid>, ITenantEntity
{
    public Guid UserId { get; set; }

    [NotNull]
    public User? User { get; set; }

    public int TenantId { get; set; }
}
