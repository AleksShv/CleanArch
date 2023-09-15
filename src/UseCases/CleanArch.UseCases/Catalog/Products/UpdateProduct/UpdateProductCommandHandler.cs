using AutoMapper;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using CleanArch.Infrastructure.Contracts.UserProvider;
using CleanArch.UseCases.Common.Handlers;
using CleanArch.DomainServices.Catalog.Services;
using CleanArch.UseCases.Internal.Exceptions;

namespace CleanArch.UseCases.Catalog.Products.UpdateProduct;

internal class UpdateProductCommandHandler : UserProvidedRequestHandler<UpdateProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IApplicationDbContext context, ICurrentUserProvider userProvider, IMapper mapper)
        : base(userProvider)
    {
        _context = context;
        _mapper = mapper;
    }

    public override async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FindByIdOrDefaultAsync(request.ProductId, cancellationToken)
            ?? throw new ProductNotFoundException(request.ProductId);

        if (!product.CheckProductOwner(UserId, Role))
        {
            throw new ProductOwningException(UserId, request.ProductId);
        }

        _mapper.Map(request, product);

        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}