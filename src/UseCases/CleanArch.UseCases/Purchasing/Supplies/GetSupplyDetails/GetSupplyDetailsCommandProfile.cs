using AutoMapper;

using CleanArch.Entities;
using CleanArch.UseCases.Catalog.Products.GetProductDetails;

namespace CleanArch.UseCases.Purchasing.Supplies.GetSupplyDetails;

internal class GetSupplyDetailsCommandProfile : Profile
{
    public GetSupplyDetailsCommandProfile()
    {
        CreateMap<Supply, SupplyDetailsDto>();
        CreateMap<Vendor, SupplyVendorDetailsDto>();
        CreateMap<Warehouse, SupplyWarehouseDetailsDto>();
        CreateMap<Product, ProductDetailsDto>();
    }
}
