using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Controllers;

public static class DependencyInjection
{
    public static IServiceCollection AddPublicApiControllers(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
