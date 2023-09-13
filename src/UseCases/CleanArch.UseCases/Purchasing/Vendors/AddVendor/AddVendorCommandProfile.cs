using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Purchasing.Vendors.AddVendor;

internal class AddVendorCommandProfile : Profile
{
    public AddVendorCommandProfile()
    {
        CreateMap<AddVendorCommand, Vendor>();
    }
}
