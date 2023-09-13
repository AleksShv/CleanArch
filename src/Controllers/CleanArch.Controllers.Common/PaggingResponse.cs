namespace CleanArch.Controllers.Common;
public record PaggingResponse<TItem>
{
    public TItem[] Items { get; init; } = Array.Empty<TItem>();
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
    public int Total { get; init; }
    public int TotalPages { get; init; }
}
