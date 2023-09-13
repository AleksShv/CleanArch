using System.Diagnostics.CodeAnalysis;

using CleanArch.Entities.Base;

namespace CleanArch.Entities;

public class Order : Entity<Guid>
{
    public Guid CustomerId { get; set; }

    [NotNull]
    public User? Customer { get; set; }

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    public OrderStatus Status { get; set; }
}
