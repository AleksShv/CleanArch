using System.Text.Json.Serialization;

using MediatR;

using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Purchasing.Products.AddProductWarehouses;

public record AddProductWarehousesCommand(
    [property: JsonIgnore] Guid ProductId,
    Guid[] WarehousesIds) : IRequest<Guid>, IValidatableRequest;
