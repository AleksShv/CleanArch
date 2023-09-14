using System.Text.Json.Serialization;

using MediatR;

namespace CleanArch.UseCases.Purchasing.Products.PurchaseProduct;

public record PurchaseProductCommand(
    [property: JsonIgnore] Guid ProductId,
    Guid WarehouseId,
    int Quantity) : IRequest<Guid>;
