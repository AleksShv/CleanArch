namespace CleanArch.Infrastructure.Contracts.MultiTenancy;

public interface ITenantProvider
{
    Tenant GetTenant();
}

public record Tenant(
    int Id,
    string Name,
    string ConnectionString);
