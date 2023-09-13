using MediatR;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouses;

public record GetWarehousesListQuery : IRequest<WarehouseListItemDto[]>;
