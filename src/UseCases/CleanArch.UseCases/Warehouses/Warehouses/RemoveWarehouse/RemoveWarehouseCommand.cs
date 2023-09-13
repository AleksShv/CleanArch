using MediatR;

namespace CleanArch.UseCases.Warehouses.Warehouses.RemoveWarehouse;

public record RemoveWarehouseCommand(Guid WarehouseId) : IRequest;
