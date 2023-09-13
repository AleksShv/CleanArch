using Microsoft.AspNetCore.Http;

namespace CleanArch.Controllers.Catalog.Products.Requests;

public record UploadProductImagesRequest(
    IFormFile ImageFile,
    int Order);