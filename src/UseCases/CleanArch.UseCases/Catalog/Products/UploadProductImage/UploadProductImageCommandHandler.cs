using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;
using CleanArch.Infrastructure.Contracts.BlobStorage;

namespace CleanArch.UseCases.Catalog.Products.UploadProductImage;

internal class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductBlobStorage _blobStorage;
    private readonly IMapper _mapper;

    public UploadProductImageCommandHandler(IApplicationDbContext context, IProductBlobStorage blobStorage, IMapper mapper)
    {
        _context = context;
        _blobStorage = blobStorage;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
    {
        Guid imageId;

        using (var imageSource = request.Source)
        {
            var productImage = _mapper.Map<ProductImage>(request);

            await _context.ProductImages.AddAsync(productImage, cancellationToken);
            await _blobStorage.UploadImageAsync(productImage.Id, productImage.FileName, imageSource, cancellationToken);


            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                await _blobStorage.DeleteImageAsync(productImage.Id, cancellationToken);
                throw;
            }

            imageId = productImage.Id;
        }

        return imageId;
    }
}
