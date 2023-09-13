namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehousesList;

public record WarehouseListItemDto(
    Guid Id,
    string Location,
    string Address);
