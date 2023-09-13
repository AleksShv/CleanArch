using MediatR;

namespace CleanArch.UseCases.Purchasing.Products.PurchaseProduct;

public record PurchaseProductCommand(
    Guid ProductId, 
    int Quantity) : IRequest<Guid>;
