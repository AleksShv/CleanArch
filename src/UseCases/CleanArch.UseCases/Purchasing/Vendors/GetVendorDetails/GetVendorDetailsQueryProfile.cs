using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Purchasing.Vendors.GetVendorDetails;

internal class GetVendorDetailsQueryProfile : Profile
{
    public GetVendorDetailsQueryProfile()
    {
        CreateMap<Vendor, VendorDetailsDto>();
    }
}
