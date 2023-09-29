using AutoMapper;
using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using CleanArch.UseCases.Internal.Exceptions;

namespace CleanArch.UseCases.Catalog.Products.UpdateProduct;

internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;

    public UpdateProductCommandHandler(IApplicationDbContext context, IMapper mapper, IPublisher publisher)
    {
        _context = context;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FindByIdOrDefaultAsync(request.ProductId, cancellationToken)
            ?? throw new ProductNotFoundException(request.ProductId);

        _mapper.Map(request, product);

        await _context.SaveChangesAsync(cancellationToken);
        await _publisher.Publish(new ProductUpdatedEvent(product), cancellationToken);

        return product.Id;
    }
}