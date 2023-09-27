using CleanArch.Entities.Base;
using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Entities;

public class BasketItem : TenantEntity<Guid>
{
    public Guid ProductId { get; set; }

    [NotNull]
    public Product? Product { get; set; }

    public Guid BasketId { get; set; }

    [NotNull]
    public Basket? Basket { get; set; }

    public int Quantity { get; set; }
}
