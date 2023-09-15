using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.Infrastructure.Contracts.BlobStorage;
using CleanArch.UseCases.Internal.Exceptions;

namespace CleanArch.UseCases.Catalog.Products.GetProductImage;

internal class GetProductImageQueryHandler : IRequestHandler<GetProductImageQuery, ProductImageDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductBlobStorage _blobStorage;

    public GetProductImageQueryHandler(IApplicationDbContext context, IProductBlobStorage blobStorage)
    {
        _context = context;
        _blobStorage = blobStorage;
    }

    public async Task<ProductImageDto> Handle(GetProductImageQuery request, CancellationToken cancellationToken)
    {
        var fileName = await _context.ProductImages
            .Where(x => x.Id == request.ImageId)
            .Select(x => x.FileName)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ProductImageNotFoundException(request.ImageId);

        var ms = new MemoryStream();
        await _blobStorage.DownloadImageAsync(request.ImageId, ms, cancellationToken);
        ms.Seek(0, SeekOrigin.Begin);

        return new(request.ImageId, fileName, ms);
    }
}
