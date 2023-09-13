using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Catalog.Products.GetProductsPage;

internal sealed class GetProductsPageQueryProfile : Profile
{
    public GetProductsPageQueryProfile()
    {
        CreateMap<Product, ProductPaggingItemDto>()
            .ForMember(d => d.Images, o => o.MapFrom(s => s.Images.OrderBy(i => i.Order)));

        CreateMap<ProductImage, ImagePaggingItemDto>();
    }
}
