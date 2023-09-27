using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;

using CleanArch.DataAccess.Contracts;
using CleanArch.DataAccess.SqlServer.Interceptors;
using CleanArch.Infrastructure.Contracts.MultiTenancy;

namespace CleanArch.DataAccess.SqlServer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddSingleton<MultiTenancyInterceptor>();

        services.Scan(scan => scan
            .FromCallingAssembly()
                .AddClasses(c => c.AssignableTo<IInterceptor>())
                    .AsSelf()
                    .WithSingletonLifetime()
                .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

        services.AddPooledDbContextFactory<ApplicationDbContext>((provider, options) =>
        {
            var connectionString = provider
                .GetRequiredService<ITenantProvider>()
                .GetTenant()
                .ConnectionString;

            options.UseSqlServer(connectionString)
                .AddInterceptors(
                    provider.GetRequiredService<AuditEntitiesInterceptor>(),
                    provider.GetRequiredService<EntitiesHistoryInterceptor>(),
                    provider.GetRequiredService<MultiTenancyInterceptor>())
                .EnableSensitiveDataLogging();
        });
        services.AddScoped<TenantDbContextFactory>();
        services.AddScoped<IApplicationDbContext>(sp => sp
            .GetRequiredService<TenantDbContextFactory>()
            .CreateDbContext());

        return services;
    }
}