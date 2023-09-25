using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using CleanArch.Entities.Base;
using CleanArch.Infrastructure.Contracts.UserProvider;

namespace CleanArch.DataAccess.SqlServer.Interceptors;

internal class AuditEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserProvider _userProvider;

    public AuditEntitiesInterceptor(ICurrentUserProvider userProvider)
    {
        _userProvider = userProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context!);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context!);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext context)
    {
        var auditableEntries = context.ChangeTracker
            .Entries<IAuditEntity>();

        string user;

        try
        {
            user = _userProvider.GetUserId();
        }
        catch
        {
            user = "System";
        }

        var now = DateTimeOffset.UtcNow;

        foreach (var entry in auditableEntries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedAt).CurrentValue = now;
                entry.Property(x => x.CreatedBy).CurrentValue = user;
                entry.Property(x => x.LastModifiedAt).CurrentValue = now;
                entry.Property(x => x.LastModifiedBy).CurrentValue = user;
            }

            else if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.LastModifiedAt).CurrentValue = now;
                entry.Property(x => x.LastModifiedBy).CurrentValue = user;
            }
        }

        var softDeletedEntries = context.ChangeTracker
            .Entries<ISoftDeletableEntity>();

        foreach (var entry in softDeletedEntries)
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.Property(x => x.IsDeleted).CurrentValue = true;
                entry.Property(x => x.DeletedAt).CurrentValue = now;
                entry.Property(x => x.DeletedBy).CurrentValue = user;
            }
        }
    }
}