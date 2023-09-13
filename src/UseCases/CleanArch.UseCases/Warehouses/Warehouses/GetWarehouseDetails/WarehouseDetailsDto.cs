namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseDetails;

public record WarehouseDetailsDto(
    Guid Id,
    string Name,
    string Location,
    string Address);