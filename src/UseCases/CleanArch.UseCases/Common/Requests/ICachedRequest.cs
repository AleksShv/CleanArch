using System.Text.Json.Serialization;

using MediatR;

namespace CleanArch.UseCases.Common.Requests;

internal interface ICachedRequest : IBaseRequest
{
    [JsonIgnore] public string? CacheKey { get; }
    [JsonIgnore] public TimeSpan? LifeTime { get; }
}
