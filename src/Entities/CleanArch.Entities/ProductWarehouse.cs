using CleanArch.Entities.Base;
using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Entities;

public class ProductWarehouse : ITenantEntity
{
    public Guid ProductId { get; set; }

    [NotNull]
    public Product? Product { get; set; }

    public Guid WarehouseId { get; set; }

    [NotNull]
    public Warehouse? Warehouse { get; set; }

    public int Quantity { get; set; }

    public int TenantId {  get; set; }
}
