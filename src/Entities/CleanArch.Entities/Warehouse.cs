using CleanArch.Entities.Base;

namespace CleanArch.Entities;

public class Warehouse : Entity<Guid>
{
    public string Name { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string Address { get; set; } = default!;

    public List<Product> Products { get; set; } = new List<Product>();
    public List<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();

    public List<Supply> Supplies { get; set; } = new List<Supply>();
}
