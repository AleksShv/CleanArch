using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Catalog.Products.GetProductDetails;

internal class GetProductDetailsQueryProfile : Profile
{
    public GetProductDetailsQueryProfile()
    {
        CreateMap<Product, ProductDetailsDto>();
        CreateMap<ProductImage, ProductImageDetailsDto>();
        CreateMap<User, ProductOwnerDetailsDto>();
    }
}
