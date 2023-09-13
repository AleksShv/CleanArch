namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouses;

public record WarehouseListItemDto(
    Guid Id,
    string Location,
    string Address);
