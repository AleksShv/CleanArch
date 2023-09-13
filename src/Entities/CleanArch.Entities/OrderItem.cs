using CleanArch.Entities.Base;
using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Entities;

public class OrderItem : Entity<Guid>
{
    public Guid ProductId { get; set; }

    [NotNull]
    public Product? Product { get; set; }

    public Guid OrderId { get; set; }

    [NotNull]
    public Order? Order { get; set; }

    public int Quantity { get; set; }
}
