using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;

using CleanArch.Infrastructure.Implementations.BlobStorage;
using CleanArch.Infrastructure.Implementations.UserProvider;
using CleanArch.Infrastructure.Implementations.Authentication;
using CleanArch.Infrastructure.Implementations.Authentication.Options;
using CleanArch.Infrastructure.Implementations.UserProvider.Options;
using CleanArch.Infrastructure.Contracts.BlobStorage;
using CleanArch.Infrastructure.Contracts.UserProvider;
using CleanArch.Infrastructure.Contracts.Authentication;

namespace CleanArch.Infrastructure.Implementations;

public static class DependencyInjections
{
    public static IServiceCollection AddBlobStorage(this IServiceCollection services, BlobStorageSettings settings)
    {
        return services
            .AddScoped(_ => new MongoClient(settings.Connectionstring).GetDatabase(settings.DatabaseName))
            .AddScoped<IProductBlobStorage, ProductBlobStorage>();
    }

    public static IServiceCollection AddUserProvider(this IServiceCollection services, Action<UserSettings> configure) 
    {
        return services
            .Configure(configure)
            .AddHttpContextAccessor()
            .AddScoped<ICurrentUserProvider, HttpCurrentUserProvider>();
    }

    public static IServiceCollection AddApplicationAuth(this IServiceCollection services)
    {
        services
            .ConfigureOptions<ConfigureJwtSettings>()
            .ConfigureOptions<ConfigureJwtBearerOptions>();

        services
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<IJwtProvider, JwtProvider>()
            .AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddAuthorization(options =>
        {
            var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser();

            options.DefaultPolicy = policy.Build();
        });

        return services;
    }
}
