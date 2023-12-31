﻿using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehousesList;

internal class GetWarehousesListQueryProfile : Profile
{
    public GetWarehousesListQueryProfile()
    {
        CreateMap<Warehouse, WarehouseListItemDto>();
    }
}
