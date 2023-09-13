using MediatR;

using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Purchasing.Products.UpdateProductPurchasingDetails;

public record UpdateProductPurchasingDetailsCommand(
    Guid ProductId,
    Guid VendorId,
    decimal Cost) : IRequest<Guid>, IValidatableRequest;
