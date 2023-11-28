using System.Reflection;
using System.Text.Json.Serialization;

using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Controllers;

public static class DependencyInjection
{
    public static IServiceCollection AddPublicApiControllers(this IServiceCollection services)
    {
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        })
        .AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
