using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Warehouses.Exception;

internal class WarehouseNotFoundExceptions : UseCaseException
{
    public WarehouseNotFoundExceptions(Guid warehouseId)
        : base("Warehouse not found")
    {
        Data["WarehouseId"] = warehouseId;
    }
}
