using AutoMapper;
using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;
using CleanArch.DomainServices.Catalog.Services;

namespace CleanArch.UseCases.Catalog.Products.AddProduct;

internal sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddProductCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        
        product.SetSKU();

        await _context.Products
            .AddAsync(product, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
