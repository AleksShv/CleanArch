using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using CleanArch.DomainServices.Purchasing.Services;

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
            ?? throw new InvalidOperationException();

        var productWarehouse = await _context.ProductWarehouses
            .FindAsync(new object[] { supply.ProductId, supply.WarehouseId }, cancellationToken);

        supply.Complete(ref productWarehouse);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
