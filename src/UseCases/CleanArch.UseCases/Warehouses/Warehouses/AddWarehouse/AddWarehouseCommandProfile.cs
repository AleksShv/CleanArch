using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Warehouses.Warehouses.AddWarehouse;

internal class AddWarehouseCommandProfile : Profile
{
    public AddWarehouseCommandProfile()
    {
        CreateMap<AddWarehouseCommand, Warehouse>();
    }
}
