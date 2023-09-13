using CleanArch.Entities;

namespace CleanArch.DomainServices.Warehouse.Services;

public static class ProductManager
{
    public static void SetSKU(this Product product)
    {
        product.SKU = SKUGenerator.Generate();
    }
}
