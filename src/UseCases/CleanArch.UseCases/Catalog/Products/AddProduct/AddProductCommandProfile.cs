using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Catalog.Products.AddProduct;

internal sealed class AddProductCommandProfile : Profile
{
    public AddProductCommandProfile()
    {
        CreateMap<AddProductCommand, Product>();
    }
}
