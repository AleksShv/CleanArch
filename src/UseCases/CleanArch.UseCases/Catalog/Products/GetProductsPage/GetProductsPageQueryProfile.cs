using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Catalog.Products.GetProductsPage;

internal sealed class GetProductsPageQueryProfile : Profile
{
    public GetProductsPageQueryProfile()
    {
        CreateMap<Product, ProductPaggingItemDto>()
            .ForMember(dest => dest.Images, opts => opts.MapFrom(src => src.Images.OrderBy(i => i.Order)));

        CreateMap<ProductImage, ImagePaggingItemDto>();
    }
}
