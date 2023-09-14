using AutoMapper;

using CleanArch.Entities;

namespace CleanArch.UseCases.Purchasing.Products.PurchaseProduct;

internal class PurchaseProductCommandProfile : Profile
{
    public PurchaseProductCommandProfile()
    {
        CreateMap<PurchaseProductCommand, Supply>();
    }
}
