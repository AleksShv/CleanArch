using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Catalog.Exceptions;

public class ProductNotFoundException : UseCaseException
{
    public ProductNotFoundException(Guid id)
        : base("Product not found")
    {
        Data["ProductId"] = id;
    }
}
