namespace CleanArch.Controllers.Catalog.Products.Responses;

public record ProductPaggingItemResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    ProductImagePaggingItemResponse[] Images);

public record ProductImagePaggingItemResponse(
    Guid Id,
    string FileName,
    int Order);