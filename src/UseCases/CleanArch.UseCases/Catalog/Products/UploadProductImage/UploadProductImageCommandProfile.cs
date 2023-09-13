using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Catalog.Products.UploadProductImage;

internal class UploadProductImageCommandProfile : Profile
{
    public UploadProductImageCommandProfile()
    {
        CreateMap<UploadProductImageCommand, ProductImage>()
            .ForMember(d => d.Ext, o => o.MapFrom(s => Path.GetExtension(s.FileName)))
            .ForMember(d => d.Size, o => o.MapFrom(s => s.Source.Length));
    }
}
