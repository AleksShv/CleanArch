using AutoMapper;
using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;
using CleanArch.DomainServices.Catalog.Services;
using CleanArch.Infrastructure.Contracts.UserProvider;

namespace CleanArch.UseCases.Catalog.Products.AddProduct;

internal sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserProvider _currentUserProvider;

    public AddProductCommandHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserProvider currentUserProvider)
    {
        _context = context;
        _mapper = mapper;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        product.SetSKU();
        product.OwnerId = _currentUserProvider.GetUserId<Guid>();

        await _context.Products
            .AddAsync(product, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
