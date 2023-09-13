using CleanArch.Entities;

namespace CleanArch.UseCases.InternalServices.Contracts;

internal interface ICatalogAccessService
{
    Task<bool> CheckUserProductAccessAsync(Guid productId, Guid userId, UserRole userRole, CancellationToken cancellationToken = default);
    Task CheckUserProductAccessAndThrowAsync(Guid productId, Guid userId, UserRole userRole, CancellationToken cancellationToken = default);

    Task<bool> CheckUserProductImageAccessAsync(Guid productImageId, Guid userId, UserRole userRole, CancellationToken cancellationToken = default);
    Task CheckUserProductImageAccessAndThrowAsync(Guid productImageId, Guid userId, UserRole userRole, CancellationToken cancellationToken = default);
}
