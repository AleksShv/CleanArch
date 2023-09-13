using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Catalog.Products.UpdateProductImageOrder;

internal class UpdateProductImageOrderCommandProfile : Profile
{
    public UpdateProductImageOrderCommandProfile()
    {
        CreateMap<UpdateProductImageOrderCommand, ProductImage>()
            .ForMember(s => s.Id, o => o.MapFrom(d => d.ImageId));
    }
}
