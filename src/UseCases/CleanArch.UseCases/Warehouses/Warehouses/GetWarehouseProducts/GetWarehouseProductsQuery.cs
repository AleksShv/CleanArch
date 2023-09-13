using MediatR;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseProducts;

public record GetWarehouseProductsQuery(Guid WarehouseId) : IRequest<ProductDetailsDto[]>;
