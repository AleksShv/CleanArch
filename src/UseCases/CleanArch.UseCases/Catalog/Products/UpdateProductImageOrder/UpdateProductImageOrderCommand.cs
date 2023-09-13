using MediatR;

namespace CleanArch.UseCases.Catalog.Products.UpdateProductImageOrder;

public record UpdateProductImageOrderCommand(Guid ImageId, int Order) : IRequest<Guid>;