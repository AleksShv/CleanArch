namespace CleanArch.UseCases.Catalog.Products.GetProductDetails;

public record ProductDetailsDto(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    string SKU,
    ProductImageDetailsDto[] Images,
    ProductOwnerDetailsDto Owner);

public record ProductImageDetailsDto(
    Guid Id,
    string FileName,
    int Order);

public record ProductOwnerDetailsDto(
    Guid Id,
    string FirstName,
    string? LastName,
    string Email);
