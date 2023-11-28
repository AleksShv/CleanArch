using System.Diagnostics.CodeAnalysis;

using CleanArch.Entities.Base;

namespace CleanArch.Entities;

public class ProductImage : FileEntity<Guid>, ITenantEntity
{
    public int Order { get; set; }

    public Guid ProductId { get; set; }

    [NotNull]
    public Product? Product { get; set; }

    public int TenantId {  get; set; }
}
