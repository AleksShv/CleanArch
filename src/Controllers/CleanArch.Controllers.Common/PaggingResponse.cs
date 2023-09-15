namespace CleanArch.Controllers.Common;

public record PaggingResponse<TItem>(
    TItem[] Items,
    int PageIndex,
    int PageSize,
    int Total,
    int TotalPages);