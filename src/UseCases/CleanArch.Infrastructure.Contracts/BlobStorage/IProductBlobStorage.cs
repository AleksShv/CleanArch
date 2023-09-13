namespace CleanArch.Infrastructure.Contracts.BlobStorage;

public interface IProductBlobStorage
{
    Task UploadImageAsync(Guid id, string fileName, Stream source, CancellationToken cancellationToken = default);
    Task DownloadImageAsync(Guid id, Stream destination, CancellationToken cancellationToken = default);
    Task DeleteImageAsync(Guid id, CancellationToken cancellationToken = default);
}
