using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.InternalServices.Contracts;
using CleanArch.UseCases.Common.Handlers;
using CleanArch.Infrastructure.Contracts.UserProvider;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Catalog.Products.RemoveProduct;

internal class RemoveProductCommandHandler : UserProvidedRequestHandler<RemoveProductCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICatalogAccessService _productAccessService;

    public RemoveProductCommandHandler(IApplicationDbContext context, ICatalogAccessService productAccessService, ICurrentUserProvider userProvider)
        : base(userProvider)
    {
        _context = context;
        _productAccessService = productAccessService;
    }

    public override async Task Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        await _productAccessService
            .CheckUserProductAccessAndThrowAsync(request.ProductId, UserId, Role, cancellationToken);

        await _context.Products
            .WithId(request.ProductId)
            .ExecuteSoftDeleteAsync(
                deletedAt: DateTimeOffset.UtcNow,
                deletedBy: UserId.ToString(),
                cancellationToken);
    }
}
