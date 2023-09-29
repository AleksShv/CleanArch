using MediatR;

using CleanArch.Entities;
using CleanArch.UseCases.Common.Requests;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Catalog.Products.GetProductDetails;
public record GetProductDetailsQuery(Guid Id) : IRequest<ProductDetailsDto>, ICachedRequest
{
    public string? CacheKey { get; } = CacheKeyGenerator.GenerateKey<Product>(Id);
    public TimeSpan? LifeTime { get; } = TimeSpan.FromMinutes(1);
}
