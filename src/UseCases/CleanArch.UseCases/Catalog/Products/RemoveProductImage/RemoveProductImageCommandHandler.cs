using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.Infrastructure.Contracts.BlobStorage;
using CleanArch.UseCases.Common.Handlers;
using CleanArch.Infrastructure.Contracts.UserProvider;
using CleanArch.UseCases.Internal.Services.Contracts;

namespace CleanArch.UseCases.Catalog.Products.RemoveProductImage;

internal class RemoveProductImageCommandHandler : UserProvidedRequestHandler<RemoveProductImageCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductBlobStorage _blobStorage;
    private readonly ICatalogAccessService _productAccessService;

    public RemoveProductImageCommandHandler(
        IProductBlobStorage blobStorage,
        IApplicationDbContext context,
        ICurrentUserProvider userProvider,
        ICatalogAccessService productAccessService)
        : base(userProvider)
    {
        _blobStorage = blobStorage;
        _context = context;
        _productAccessService = productAccessService;
    }

    public override async Task Handle(RemoveProductImageCommand request, CancellationToken cancellationToken)
    {
        await _productAccessService
            .CheckUserProductImageAccessAndThrowAsync(request.ImageId, UserId, Role, cancellationToken);

        await _context.ProductImages
            .Where(pi => pi.Id == request.ImageId)
            .ExecuteDeleteAsync(cancellationToken);

        await _blobStorage.DeleteImageAsync(request.ImageId, cancellationToken);
    }
}
