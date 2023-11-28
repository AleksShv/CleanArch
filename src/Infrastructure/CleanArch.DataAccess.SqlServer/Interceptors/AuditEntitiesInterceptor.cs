using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using CleanArch.Entities.Base;
using CleanArch.Infrastructure.Contracts.UserProvider;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.DataAccess.SqlServer.Interceptors;

internal class AuditEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly IServiceProvider _serviceProvider;

    public AuditEntitiesInterceptor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
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

        if (!auditableEntries.Any())
        {
            return;
        }

        string user;
        using (var scope = _serviceProvider.CreateScope())
        {
            try
            {
                user = scope.ServiceProvider
                    .GetRequiredService<ICurrentUserProvider>()
                    .GetUserId();
            }
            catch
            {
                user = "System";
            }
        }

        var now = DateTimeOffset.UtcNow;

        foreach (var entry in auditableEntries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(x => x.CreatedAt).CurrentValue = now;
                    entry.Property(x => x.CreatedBy).CurrentValue = user;
                    entry.Property(x => x.LastModifiedAt).CurrentValue = now;
                    entry.Property(x => x.LastModifiedBy).CurrentValue = user;
                    break;
                case EntityState.Modified:
                    entry.Property(x => x.LastModifiedAt).CurrentValue = now;
                    entry.Property(x => x.LastModifiedBy).CurrentValue = user;
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        var softDeletedEntries = context.ChangeTracker
            .Entries<ISoftDeletableEntity>();

        foreach (var entry in softDeletedEntries)
        {
            if (entry.State != EntityState.Deleted) continue;
            
            entry.Property(x => x.IsDeleted).CurrentValue = true;
            entry.Property(x => x.DeletedAt).CurrentValue = now;
            entry.Property(x => x.DeletedBy).CurrentValue = user;
        }
    }
}