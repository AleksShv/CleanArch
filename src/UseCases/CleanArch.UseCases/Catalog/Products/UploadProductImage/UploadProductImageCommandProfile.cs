using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Catalog.Products.UploadProductImage;

internal class UploadProductImageCommandProfile : Profile
{
    public UploadProductImageCommandProfile()
    {
        CreateMap<UploadProductImageCommand, ProductImage>()
            .ForMember(src => src.Ext, opts => opts.MapFrom(dest => Path.GetExtension(dest.FileName)))
            .ForMember(src => src.Size, opts => opts.MapFrom(dest => dest.Source.Length));
    }
}
