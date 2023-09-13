using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Purchasing.Products.RemoveProductWarehouses;

internal class RemoveProductWarehousesCommandHandler : IRequestHandler<RemoveProductWarehousesCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveProductWarehousesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveProductWarehousesCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Select(p => new Product
            {
                Id = request.ProductId,
                Warehouses = p.Warehouses
                    .Select(w => new Warehouse { Id = w.Id })
                    .ToList()
            })
            .FindByIdAsync(request.ProductId, cancellationToken);

        _context.Products.Attach(product);

        var warehouses = request.WarehousesIds
            .Where(id => product.Warehouses.Any(w => w.Id == id))
            .Select(id => new Warehouse { Id = id });

        foreach (var warehouse in warehouses)
        {
            product.Warehouses.Remove(warehouse);
        }
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}
