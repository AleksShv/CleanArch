using MediatR;

namespace CleanArch.UseCases.Warehouses.Warehouses.AddWarehouse;

public record AddWarehouseCommand(
    string Name,
    string Location,
    string Address) : IRequest<Guid>;
