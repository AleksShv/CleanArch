using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using CleanArch.Infrastructure.Contracts.MultiTenancy;

namespace CleanArch.Infrastructure.Implementations.MultiTenancy;

internal class HttpContextTenantProvider : ITenantProvider
{
    private const int DefaultTenant = 1;

    private readonly IHttpContextAccessor _contextAccessor;
    private readonly MultiTenancySettings _settings;

    public HttpContextTenantProvider(IHttpContextAccessor contextAccessor, IOptions<MultiTenancySettings> options)
    {
        _contextAccessor = contextAccessor;
        _settings = options.Value;
    }

    public Tenant GetTenant()
    {
        var headers = _contextAccessor.HttpContext?.Request?.Headers;

        var tenantId = DefaultTenant;
        if (int.TryParse(headers?[_settings.TenantHeaderName], out var id))
        {
            tenantId = id;
        }

        var tenants = Array.FindAll(_settings.Tenants, t => t.Id == tenantId);
        if (tenants.Length > 1)
        {
            throw new InvalidOperationException("Not unique tenant id");
        }
        var tenant = tenants[0];

        var connection = string.IsNullOrWhiteSpace(tenant.ConnectionString)
            ? _settings.DefaultConnection
            : tenant.ConnectionString;

        return new(tenant.Id, tenant.Name, connection);
    }
}
