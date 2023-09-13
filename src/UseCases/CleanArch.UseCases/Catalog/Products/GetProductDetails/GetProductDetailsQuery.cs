using MediatR;

namespace CleanArch.UseCases.Catalog.Products.GetProductDetails;
public record GetProductDetailsQuery(Guid Id) : IRequest<ProductDetailsDto>;
