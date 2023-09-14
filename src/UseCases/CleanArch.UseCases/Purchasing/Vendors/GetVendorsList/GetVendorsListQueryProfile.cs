using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Purchasing.Vendors.GetVendorsList;

internal class GetVendorsListQueryProfile : Profile
{
    public GetVendorsListQueryProfile()
    {
        CreateMap<Vendor, VendorListItemDto>();
    }
}
