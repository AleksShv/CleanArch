using DelegateDecompiler;

using CleanArch.Entities;

namespace CleanArch.DomainServices.Warehouse.Utils;

public static class ProductQueries
{
    [Computed]
    public static bool FormWarehouse(this Product product, Guid warehouseId)
        => product.Warehouses.Any(w => w.Id == warehouseId);
}
