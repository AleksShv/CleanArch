using MediatR;
using Microsoft.Extensions.Caching.Distributed;

using CleanArch.Entities;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Catalog.Products.UpdateProduct;

public class RemoveCacheOnProductUpdatedHandler : INotificationHandler<ProductUpdatedEvent>
{
    private readonly IDistributedCache _cache;

    public RemoveCacheOnProductUpdatedHandler(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var key = CacheKeyGenerator.GenerateKey<Product>(notification.Product.Id);
        await _cache.RemoveAsync(key, cancellationToken);
    }
}
