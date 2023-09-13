using AutoMapper;

using CleanArch.Entities;
using CleanArch.UseCases.Warehouses.Warehouses.GetWarehouses;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehousesList;

internal class GetWarehousesListQueryProfile : Profile
{
    public GetWarehousesListQueryProfile()
    {
        CreateMap<Warehouse, WarehouseListItemDto>();
    }
}
