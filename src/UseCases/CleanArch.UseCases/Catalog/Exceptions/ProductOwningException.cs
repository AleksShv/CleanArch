using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Catalog.Exceptions;

internal class ProductOwningException : UseCaseException
{
    public ProductOwningException(Guid userId, Guid productId)
        : base("You aren't owner of the product")
    {
        Data["UserId"] = userId;
        Data["ProductId"] = productId;
    }
}
