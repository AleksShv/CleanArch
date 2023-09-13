using MediatR;
using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Catalog.Products.AddProduct;

public record AddProductCommand(
    string Title,
    string Description,
    decimal Price)
    : IRequest<Guid>, IValidatableRequest;
