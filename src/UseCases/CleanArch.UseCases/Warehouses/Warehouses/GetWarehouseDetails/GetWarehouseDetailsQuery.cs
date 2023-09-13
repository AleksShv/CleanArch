using MediatR;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseDetails;

public record GetWarehouseDetailsQuery(Guid WarehouseId) : IRequest<WarehouseDetailsDto?>;
