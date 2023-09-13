using System.Text.Json.Serialization;

namespace CleanArch.Controllers.Catalog.Products.Requests;

public record UpdateProductImageOrderRequest(
    [property: JsonIgnore] Guid ImageId,
    int Order);