using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

using CleanArch.Entities.Base;
using CleanArch.Infrastructure.Contracts.MultiTenancy;

namespace CleanArch.DataAccess.SqlServer.Interceptors;

internal class MultiTenancyInterceptor : SaveChangesInterceptor
{
    private readonly ITenantProvider _tenantProvider;

    public MultiTenancyInterceptor(ITenantProvider tenantProvider)
    {
        _tenantProvider = tenantProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if (context is null)
        {
            return base.SavingChanges(eventData, result);
        }

        FillTenants(context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        FillTenants(context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void FillTenants(DbContext context)
    {
        var tenantEntries = context.ChangeTracker
            .Entries<ITenantEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in tenantEntries)
        {
            var property = entry.Property(x => x.TenantId);

            if (property.CurrentValue == default)
            {
                property.CurrentValue = _tenantProvider.GetTenant().Id;
            }
        }
    }
}
