using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.Infrastructure.Contracts.UserProvider;
using CleanArch.Entities;

namespace CleanArch.UseCases.Catalog.Products.PutProductOnSale;

internal sealed class PutProductOnSaleCommandHandler : IRequestHandler<PutProductOnSaleCommand>
{
    private readonly IApplicationDbContext _context;

    public PutProductOnSaleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PutProductOnSaleCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.ProductId,
            IsAvailableForSale = true
        };

        _context.Products.Attach(product);
        _context.Products.Entry(product).Property(x => x.IsAvailableForSale).IsModified = true;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
