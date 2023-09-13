using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using CleanArch.DataAccess.Contracts;
using CleanArch.DataAccess.SqlServer.Interceptors;
using CleanArch.DataAccess.SqlServer.QueryServices;

namespace CleanArch.DataAccess.SqlServer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        services.AddSingleton<AuditableEntitiesInterceptor>();
        
        services.AddDbContextPool<ApplicationDbContext>((provider, options) =>
        {
            options.UseSqlServer(connectionString)
                .AddInterceptors(
                    provider.GetRequiredService<AuditableEntitiesInterceptor>());
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddTransient<IProductQueryService, SqlServerProductQueryService>();

        return services;
    }
}
