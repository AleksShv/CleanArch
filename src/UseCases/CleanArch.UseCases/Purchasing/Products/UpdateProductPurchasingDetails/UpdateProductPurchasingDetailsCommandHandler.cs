using AutoMapper;
using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;

namespace CleanArch.UseCases.Purchasing.Products.UpdateProductPurchasingDetails;

internal class UpdateProductPurchasingDetailsCommandHandler : IRequestHandler<UpdateProductPurchasingDetailsCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateProductPurchasingDetailsCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateProductPurchasingDetailsCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        var entry = _context.Products.Attach(product);
        entry.Property(p => p.Cost).IsModified = true;
        entry.Property(p => p.VendorId).IsModified = true;

        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
