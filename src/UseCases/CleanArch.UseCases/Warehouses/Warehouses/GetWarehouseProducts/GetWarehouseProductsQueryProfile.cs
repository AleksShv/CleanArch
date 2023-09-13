using AutoMapper;
using CleanArch.Entities;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseProducts;

internal class GetWarehouseProductsQueryProfile : Profile
{
    public GetWarehouseProductsQueryProfile()
    {
        CreateMap<Product, ProductDetailsDto>();
    }
}
