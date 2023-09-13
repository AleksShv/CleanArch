using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Purchasing.Products.UpdateProductPurchasingDetails;

internal class UpdateProductPurchasingDetailsCommandProfile : Profile
{
    public UpdateProductPurchasingDetailsCommandProfile()
    {
        CreateMap<UpdateProductPurchasingDetailsCommand, Product>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.ProductId));
    }
}
