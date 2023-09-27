using System.Diagnostics.CodeAnalysis;

using CleanArch.Entities.Base;

namespace CleanArch.Entities;

public class Basket : TenantEntity<Guid>
{
    public Guid CustomerId { get; set; }

    [NotNull]
    public User? Customer { get; set; }

    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
}
