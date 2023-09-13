namespace CleanArch.Controllers.Catalog.Products.Requests;

public record GetProductPageRequest(
    string? SearchString = null,
    int PageIndex = 0,
    int PageSize = 20);
