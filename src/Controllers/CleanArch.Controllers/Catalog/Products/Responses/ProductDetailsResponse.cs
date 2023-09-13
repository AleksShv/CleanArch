namespace CleanArch.Controllers.Catalog.Products.Responses;

public record ProductDetailsResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    string SKU,
    List<ProductImageDetailsResponse> Images,
    ProductOwnerDetailsResponse Owner);

public record ProductImageDetailsResponse(
    Guid Id,
    string FileName,
    int Order);

public record ProductOwnerDetailsResponse(
    Guid Id,
    string FirstName,
    string? LastName,
    string Email);
