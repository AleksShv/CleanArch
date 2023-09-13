using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Warehouses.Warehouses.UpdateWarehouse;

internal class UpdateWarehouseCommandProfile : Profile
{
    public UpdateWarehouseCommandProfile()
    {
        CreateMap<UpdateWarehouseCommand, Warehouse>();
    }
}
