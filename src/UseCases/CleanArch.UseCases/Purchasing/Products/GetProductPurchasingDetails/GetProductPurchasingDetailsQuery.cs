using MediatR;

namespace CleanArch.UseCases.Purchasing.Products.GetProductPurchasingDetails;

public record GetProductPurchasingDetailsQuery(Guid ProductId) : IRequest<ProductPurchasingDetailsDto?>;