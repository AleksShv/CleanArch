using CleanArch.Entities;

namespace CleanArch.DomainServices.Purchasing.Services;

public static class SupplyManager
{
    public static void Complete(this Supply supply, ref ProductWarehouse? productWarehouse)
    {
        supply.IsCompleted = true;

        if (productWarehouse is null)
        {
            productWarehouse = new ProductWarehouse
            {
                ProductId = supply.ProductId,
                WarehouseId = supply.WarehouseId,
                Quantity = supply.Quantity,
            };
        }

        else
        {
            productWarehouse.Quantity += supply.Quantity;
        }
    }
}
