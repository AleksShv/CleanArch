using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Catalog.Products.UpdateProduct;

internal class UpdateProductCommandProfile : Profile
{
    public UpdateProductCommandProfile()
    {
        CreateMap<UpdateProductCommand, Product>();
    }
}
