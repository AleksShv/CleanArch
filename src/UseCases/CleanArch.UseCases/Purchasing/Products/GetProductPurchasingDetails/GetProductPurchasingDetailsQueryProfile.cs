using AutoMapper;

using CleanArch.Entities;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Purchasing.Products.GetProductPurchasingDetails;

internal class GetProductPurchasingDetailsQueryProfile : Profile
{
    public GetProductPurchasingDetailsQueryProfile()
    {
        CreateMap<Product, ProductPurchasingDetailsDto>()
            .ForRecordParam(d => d.Warehouses, o => o.MapFrom(s => s.ProductWarehouses));
        CreateMap<Vendor, VendorPurchasingDetailsDto>();
        CreateMap<Supply, SupplyPurchasingDetailsDto>();
        CreateMap<ProductWarehouse, WarehousePurchasingDetailsDto>()
            .ForRecordParam(d => d.Id, o => o.MapFrom(s => s.WarehouseId))
            .ForRecordParam(d => d.Name, o => o.MapFrom(s => s.Warehouse.Name))
            .ForRecordParam(d => d.Location, o => o.MapFrom(s => s.Warehouse.Location))
            .ForRecordParam(d => d.Address, o => o.MapFrom(s => s.Warehouse.Address));
    }
}
    