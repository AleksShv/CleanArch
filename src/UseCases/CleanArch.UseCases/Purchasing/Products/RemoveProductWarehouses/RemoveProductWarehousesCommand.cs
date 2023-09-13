using System.Text.Json.Serialization;

using MediatR;

namespace CleanArch.UseCases.Purchasing.Products.RemoveProductWarehouses;

public record RemoveProductWarehousesCommand(
    [property: JsonIgnore] Guid ProductId,
    Guid[] WarehousesIds) : IRequest;
