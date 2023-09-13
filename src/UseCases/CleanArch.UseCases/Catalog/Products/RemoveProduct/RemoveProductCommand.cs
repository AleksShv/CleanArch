using MediatR;

using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Catalog.Products.RemoveProduct;

public record RemoveProductCommand(Guid ProductId) : IRequest, ITransactionalRequest;
