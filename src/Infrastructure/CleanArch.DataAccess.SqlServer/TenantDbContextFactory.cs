using Microsoft.EntityFrameworkCore;

using CleanArch.Infrastructure.Contracts.MultiTenancy;

namespace CleanArch.DataAccess.SqlServer;

internal class TenantDbContextFactory : IDbContextFactory<ApplicationDbContext>
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
    private readonly ITenantProvider _tenantProvider;

    public TenantDbContextFactory(IDbContextFactory<ApplicationDbContext> contextFactory, ITenantProvider tenantProvider)
    {
        _contextFactory = contextFactory;
        _tenantProvider = tenantProvider;
    }

    public ApplicationDbContext CreateDbContext()
    {
        var ctx = _contextFactory.CreateDbContext();
        ctx.TenantId = _tenantProvider.GetTenant().Id;
        return ctx;
    }
}