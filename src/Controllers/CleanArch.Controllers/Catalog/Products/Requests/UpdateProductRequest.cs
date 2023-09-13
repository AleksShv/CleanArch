using System.Text.Json.Serialization;

namespace CleanArch.Controllers.Catalog.Products.Requests;

public record UpdateProductRequest(
    [property: JsonIgnore] Guid ProductId,
    string Title,
    string Description,
    decimal Price)
    : AddProductRequest(Title, Description, Price);
