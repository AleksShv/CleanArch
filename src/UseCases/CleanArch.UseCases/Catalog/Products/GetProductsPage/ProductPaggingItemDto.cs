namespace CleanArch.UseCases.Catalog.Products.GetProductsPage;

public record ProductPaggingItemDto(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    ImagePaggingItemDto[] Images);

public record ImagePaggingItemDto(
    Guid Id,
    string FileName,
    int Order);