using CleanArch.UseCases.Catalog.Products.AddProduct;

namespace CleanArch.UseCases.Catalog.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid ProductId,
    string Title,
    string Description,
    decimal Price) : AddProductCommand(Title, Description, Price);