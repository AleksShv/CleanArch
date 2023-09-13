namespace CleanArch.Infrastructure.Contracts.BlobStorage;

public interface IAvatarBlobStorage
{
    Task UploadAvatarAsync(Guid id, string fileName, Stream source, CancellationToken cancellationToken = default);
    Task DownloadAvatarAsync(Guid id, Stream destination, CancellationToken cancellationToken = default);
    Task DeleteAvatarAsync(Guid id, CancellationToken cancellationToken = default);
}
