using System.Text.Json;

using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Common.Behaviours;

internal class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse?>
    where TRequest : ICachedRequest
{
    private readonly IDistributedCache _cache;

    public CachingBehavior(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<TResponse?> Handle(TRequest request, RequestHandlerDelegate<TResponse?> next, CancellationToken cancellationToken)
    {
        TResponse? response = default;

        var key = request.CacheKey;

        if (string.IsNullOrWhiteSpace(key))
        {
            var jsonRequest = JsonSerializer.Serialize(request);
            key = $"{typeof(TRequest).Name}_{jsonRequest.GetHashCode()}";
        }

        var stringResponse = await _cache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrWhiteSpace(stringResponse))
        {
            response = await next();
            stringResponse = JsonSerializer.Serialize(response);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = request.LifeTime
            };
            await _cache.SetStringAsync(key, stringResponse, options, cancellationToken);
        }

        response ??= JsonSerializer.Deserialize<TResponse>(stringResponse);

        return response;
    }
}
