using MediatR;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehousesList;

public record GetWarehousesListQuery : IRequest<WarehouseListItemDto[]>;
