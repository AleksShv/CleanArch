using MediatR;

using CleanArch.UseCases.Common.Models;
using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Catalog.Products.GetProductsPage;

public record GetProductsPageQuery(
    string? SearchString = null,
    int PageIndex = 0,
    int PageSize = 25)
    : IRequest<PaggingDto<ProductPaggingItemDto>>, IValidatableRequest;