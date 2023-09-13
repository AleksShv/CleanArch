using CleanArch.Entities;

namespace CleanArch.DataAccess.Contracts;

public interface IProductQueryService
{
    Task<Product?> GetProductDetailsAsync(Guid id);
}
