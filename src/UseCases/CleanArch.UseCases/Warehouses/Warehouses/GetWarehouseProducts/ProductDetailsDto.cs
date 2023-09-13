namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseProducts;

public record ProductDetailsDto(
    Guid Id,
    string SKU,
    int QuantityInStock);
