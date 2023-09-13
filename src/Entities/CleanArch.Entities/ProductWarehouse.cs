using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Entities;

public class ProductWarehouse
{
    public Guid ProductId { get; set; }

    [NotNull]
    public Product? Product { get; set; }

    public Guid WarehouseId { get; set; }

    [NotNull]
    public Warehouse? Warehouse { get; set; }

    public int Quantity { get; set; }
}
