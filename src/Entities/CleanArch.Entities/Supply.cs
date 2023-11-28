using System.Diagnostics.CodeAnalysis;

using CleanArch.Entities.Base;

namespace CleanArch.Entities;

public class Supply : TenantEntity<Guid>
{
    public Guid ProductId { get; set; }

    [NotNull]
    public Product? Product { get; set; }

    public Guid VendorId { get; set; }

    [NotNull]
    public Vendor? Vendor { get; set; }

    public Guid WarehouseId { get; set; }

    [NotNull]
    public Warehouse? Warehouse { get; set; }

    public int Quantity { get; set; }
    
    public bool IsCompleted { get; set; }
}
