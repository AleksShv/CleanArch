using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Internal.Exceptions;

public class ProductNotFoundException : UseCaseException
{
    public ProductNotFoundException(Guid id)
        : base("Product not found")
    {
        Data["ProductId"] = id;
    }
}
