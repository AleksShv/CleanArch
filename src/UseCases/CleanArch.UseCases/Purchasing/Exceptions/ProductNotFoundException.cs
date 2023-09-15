using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Purchasing.Exceptions;

internal class ProductNotFoundException : UseCaseException
{
    public ProductNotFoundException(Guid productId)
        : base("Product not found")
    {
        Data["ProductId"] = productId;
    }
}
