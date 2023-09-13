namespace CleanArch.Controllers.Catalog.Products.Requests;

public record AddProductRequest(
    string Title,
    string Description,
    decimal Price);
