using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseDetails;

internal class GetWarehouseDetailsQueryProfile : Profile
{
    public GetWarehouseDetailsQueryProfile()
    {
        CreateMap<Warehouse, WarehouseDetailsDto>();
    }
}
