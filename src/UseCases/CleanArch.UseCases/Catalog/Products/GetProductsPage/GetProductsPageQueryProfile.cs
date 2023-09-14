using AutoMapper;

using CleanArch.Entities;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Catalog.Products.GetProductsPage;

internal sealed class GetProductsPageQueryProfile : Profile
{
    public GetProductsPageQueryProfile()
    {
        CreateMap<Product, ProductPaggingItemDto>()
            .ForRecordParam(d => d.Images, o => o.MapFrom(s => s.Images.OrderBy(i => i.Order)));

        CreateMap<ProductImage, ImagePaggingItemDto>();
    }
}
