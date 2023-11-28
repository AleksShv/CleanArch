using System.ComponentModel.DataAnnotations;

namespace CleanArch.Infrastructure.Implementations.MultiTenancy;

public record MultiTenancySettings
{
    [Required(AllowEmptyStrings = false)]
    public string TenantHeaderName { get; set; } = default!;

    [Required(AllowEmptyStrings = false)]
    public string DefaultConnection { get; set; } = default!;

    public Tenant[] Tenants { get; set; } = Array.Empty<Tenant>();

    public class Tenant
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = default!;

        public string? ConnectionString { get; set; }
    }
}