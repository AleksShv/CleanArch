using System.Reflection;

using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.DependencyInjection;

using CleanArch.UseCases.Common.Behaviours;

namespace CleanArch.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.Scan(scan => scan
            .FromCallingAssembly()
                .AddClasses(c => c.GetType().Name.EndsWith("Service"))
                    .AsMatchingInterface()
                    .WithTransientLifetime()
                .AddClasses(c => c.AssignableTo(typeof(IAsyncPropertyValidator<,>)))
                    .AsSelf()
                    .WithTransientLifetime());

        services.AddAutoMapper((provider, cfg) =>
        {
            cfg.ValueTransformers
                .Add(new(typeof(string), (string v) => v.Trim()));

            cfg.ConstructServicesUsing(provider.GetRequiredService);
        }, assembly);

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            cfg.AddOpenBehavior(typeof(RoleBasedRequestBehavior<,>), ServiceLifetime.Transient);
            cfg.AddOpenBehavior(typeof(CachingBehavior<,>), ServiceLifetime.Transient);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>), ServiceLifetime.Transient);
            cfg.AddOpenBehavior(typeof(TransactionalBehavior<,>), ServiceLifetime.Scoped);
        });

        return services;
    }
}
