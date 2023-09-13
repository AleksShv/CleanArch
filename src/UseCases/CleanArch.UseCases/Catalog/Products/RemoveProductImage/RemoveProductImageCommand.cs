using CleanArch.UseCases.Common.Requests;
using MediatR;

namespace CleanArch.UseCases.Catalog.Products.RemoveProductImage;

public record RemoveProductImageCommand(Guid ImageId) : IRequest, ITransactionalRequest;
