using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Warehouses.Warehouses.RemoveWarehouse;

internal class RemoveWarehouseCommandHandler : IRequestHandler<RemoveWarehouseCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveWarehouseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveWarehouseCommand request, CancellationToken cancellationToken)
        => await _context.Warehouses
            .WithId(request.WarehouseId)
            .ExecuteDeleteAsync(cancellationToken);
}
