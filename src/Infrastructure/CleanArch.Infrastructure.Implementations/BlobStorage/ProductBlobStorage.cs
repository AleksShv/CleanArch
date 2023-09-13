using MongoDB.Driver;
using MongoDB.Driver.GridFS;

using CleanArch.Infrastructure.Contracts.BlobStorage;

namespace CleanArch.Infrastructure.Implementations.BlobStorage;

internal class ProductBlobStorage : IProductBlobStorage
{
    private readonly IGridFSBucket<Guid> _bucket;

    public ProductBlobStorage(IMongoDatabase mongoDatabase)
    {
        _bucket = new GridFSBucket<Guid>(mongoDatabase, new GridFSBucketOptions
        {
            BucketName = nameof(ProductBlobStorage),
        });
    }

    public async Task DeleteImageAsync(Guid id, CancellationToken cancellationToken = default)
        => await _bucket.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

    public async Task DownloadImageAsync(Guid id, Stream destination, CancellationToken cancellationToken = default)
        => await _bucket.DownloadToStreamAsync(id, destination, null, cancellationToken).ConfigureAwait(false);

    public async Task UploadImageAsync(Guid id, string fileName, Stream source, CancellationToken cancellationToken = default)
        => await _bucket.UploadFromStreamAsync(id, fileName, source, null, cancellationToken).ConfigureAwait(false);
}
