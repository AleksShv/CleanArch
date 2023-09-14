namespace CleanArch.UseCases.Purchasing.Supplies.GetSupplyDetails;

public record SupplyDetailsDto(
    Guid Id,
    SupplyVendorDetailsDto Vendor,
    SupplyWarehouseDetailsDto Warehouse,
    SupplyProductDetailsDto Product,
    int Quantity);

public record SupplyVendorDetailsDto(
    Guid Id,
    string Name);

public record SupplyWarehouseDetailsDto(
    Guid Id,
    string Name);

public record SupplyProductDetailsDto(
    Guid Id,
    string SKU);