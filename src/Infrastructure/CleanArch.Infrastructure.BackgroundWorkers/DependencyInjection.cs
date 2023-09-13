using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrutor;

namespace CleanArch.Infrastructure.BackgroundWorkers;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundWorkers(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromCallingAssembly()
                .AddClasses(c => c.AssignableTo<IHostedService>())
                    .UsingRegistrationStrategy(RegistrationStrategy.Append)
                    .As<IHostedService>()
                    .WithSingletonLifetime());

        return services;
    }
}
