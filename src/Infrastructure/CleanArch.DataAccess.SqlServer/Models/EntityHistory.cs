using CleanArch.Entities.Base;

namespace CleanArch.DataAccess.SqlServer.Models;

public class EntityHistory : TenantEntity<long>
{
    public string EntityType { get; set; } = default!;
    public string Originator { get; set; } = default!;
    public string Action {  get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public object? Payload { get; set; }
}

