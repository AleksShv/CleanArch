using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArch.Infrastructure.Implementations.Authentication.Options;

internal class ConfigureJwtSettings : IConfigureOptions<JwtSettings>
{
    private readonly IConfiguration _configuration;

    public ConfigureJwtSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtSettings options)
    {
        _configuration
            .GetSection(nameof(JwtSettings))
            .Bind(options);
    }
}
