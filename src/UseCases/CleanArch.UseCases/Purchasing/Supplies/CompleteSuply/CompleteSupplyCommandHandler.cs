using MediatR;

using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using CleanArch.DomainServices.Purchasing.Services;
using CleanArch.UseCases.Purchasing.Exceptions;

namespace CleanArch.UseCases.Purchasing.Supplies.CompleteSuply;

internal class CompleteSupplyCommandHandler : IRequestHandler<CompleteSupplyCommand>
{
    private readonly IApplicationDbContext _context;

    public CompleteSupplyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CompleteSupplyCommand request, CancellationToken cancellationToken)
    {
        var supply = await _context.Supplies
            .FindByIdOrDefaultAsync(request.SupplyId, cancellationToken)
            ?? throw new SupplyNotFoundException(request.SupplyId);

        var productWarehouse = await _context.ProductWarehouses
            .FindAsync(new object[] { supply.ProductId, supply.WarehouseId }, cancellationToken);

        productWarehouse = supply.Complete(productWarehouse);

        if (_context.ProductWarehouses.Entry(productWarehouse).State == EntityState.Detached)
        {
            await _context.ProductWarehouses.AddAsync(productWarehouse, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}