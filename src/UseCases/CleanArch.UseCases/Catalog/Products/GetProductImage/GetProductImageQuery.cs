using MediatR;

namespace CleanArch.UseCases.Catalog.Products.GetProductImage;

public record GetProductImageQuery(Guid ImageId) : IRequest<ProductImageDto>;
