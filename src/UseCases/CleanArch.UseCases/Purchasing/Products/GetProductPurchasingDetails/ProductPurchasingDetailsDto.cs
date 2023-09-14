namespace CleanArch.UseCases.Purchasing.Products.GetProductPurchasingDetails;

public record ProductPurchasingDetailsDto(
    Guid Id,
    string SKU,
    decimal Cost,
    int QuantityInStock,
    WarehousePurchasingDetailsDto[] Warehouses,
    VendorPurchasingDetailsDto Vendor,
    SupplyPurchasingDetailsDto[] Supplies);

public record WarehousePurchasingDetailsDto(
    Guid Id,
    string Name,
    string Location,
    string Address,
    int Quantity);

public record VendorPurchasingDetailsDto(
    Guid Id,
    string Name,
    string OGRN,
    string INN,
    string KPP);

public record SupplyPurchasingDetailsDto(
    Guid Id,
    Guid VendorId,
    Guid WarehouseId,
    int Quantity,
    bool IsCompleted);