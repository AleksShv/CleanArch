namespace CleanArch.UseCases.Catalog.Exceptions;

internal class ProductImageOwningException : Exception
{
    public ProductImageOwningException(Guid userId, Guid productImageId)
        : base("You aren't owner of the product image")
    {
        Data["UserId"] = userId;
        Data["ProductImageId"] = productImageId;
    }
}
