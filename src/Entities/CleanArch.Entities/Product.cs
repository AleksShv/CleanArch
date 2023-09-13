using System.Diagnostics.CodeAnalysis;

using CleanArch.Entities.Base;

namespace CleanArch.Entities;

public class Product : Entity<Guid>, IAuditableEntity, ISoftDeletableEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public List<ProductImage> Images { get; set; } = new List<ProductImage>();
    
    public Guid OwnerId { get; set; }
    
    [NotNull]
    public User? Owner { get; set; }

    public bool IsDraft { get; set; }
    public bool IsAvailableForSale { get; set; }

    public Guid? VendorId { get; set; }
    public Vendor? Vendor { get; set; }

    public string? SKU { get; set; } = default!;
    public decimal? Cost { get; set; }

    public int QuantityInStock { get; set; } = default;

    public List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
    public List<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>(); 

    public List<Supply> Supplies { get; set; } = new List<Supply>();

    public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public string CreatedBy { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public string LastModifiedBy { get; set; } = default!;
    public DateTimeOffset LastModifiedAt { get; set; }

    public bool IsDeleted { get; set; }
    public string? DeletedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
