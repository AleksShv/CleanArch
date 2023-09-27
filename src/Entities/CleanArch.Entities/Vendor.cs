using CleanArch.Entities.Base;

namespace CleanArch.Entities;

public class Vendor : TenantEntity<Guid>
{
    public string Name { get; set; } = default!;
    public string OGRN { get; set; } = default!;
    public string INN { get; set; } = default!;
    public string KPP { get; set; } = default!;

    public List<Product> Products { get; set; } = new List<Product>();
    public List<Supply> Supplies { get; set; } = new List<Supply>();

}
