using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using CleanArch.Entities;
using CleanArch.UseCases.Catalog.Exceptions;

namespace CleanArch.UseCases.Purchasing.Products.PurchaseProduct;

internal class PurchaseProductCommandHandler : IRequestHandler<PurchaseProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PurchaseProductCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(PurchaseProductCommand request, CancellationToken cancellationToken)
    {
        var vendorId = await _context.Products
            .WithId(request.ProductId)
            .Select(p => p.VendorId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ProductNotFoundException(request.ProductId);

        var supply = _mapper.Map<Supply>(request);
        supply.VendorId = vendorId;

        await _context.Supplies.AddAsync(supply, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return supply.Id;
    }
}
