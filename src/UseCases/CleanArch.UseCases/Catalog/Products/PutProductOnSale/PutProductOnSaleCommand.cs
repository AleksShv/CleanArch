using MediatR;

using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Catalog.Products.PutProductOnSale;

public record PutProductOnSaleCommand(Guid ProductId) : IRequest, IValidatableRequest;
