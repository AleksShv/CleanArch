namespace CleanArch.UseCases.Catalog.Products.GetProductImage;

public record ProductImageDto(Guid ImageId, string FileName, Stream Contenntt);
