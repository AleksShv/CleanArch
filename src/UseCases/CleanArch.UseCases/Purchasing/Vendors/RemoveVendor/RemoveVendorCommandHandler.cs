using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Purchasing.Vendors.RemoveVendor;

internal class RemoveVendorCommandHandler : IRequestHandler<RemoveVendorCommand>
{
    private readonly IApplicationDbContext _context;

    public RemoveVendorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveVendorCommand request, CancellationToken cancellationToken)
        => await _context.Vendors
            .WithId(request.VendorId)
            .ExecuteDeleteAsync(cancellationToken);
}
