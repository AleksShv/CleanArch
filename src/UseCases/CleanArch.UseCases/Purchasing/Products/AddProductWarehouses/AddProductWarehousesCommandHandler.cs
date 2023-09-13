using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Purchasing.Products.AddProductWarehouses;

internal class AddProductWarehousesCommandHandler : IRequestHandler<AddProductWarehousesCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public AddProductWarehousesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddProductWarehousesCommand request, CancellationToken cancellationToken)
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
            .Where(id => product.Warehouses.All(w => w.Id != id))
            .Select(id => new Warehouse { Id = id });

        foreach (var warehouse in warehouses)
        {
            _context.Warehouses.Attach(warehouse);
            warehouse.Products.Add(product);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}