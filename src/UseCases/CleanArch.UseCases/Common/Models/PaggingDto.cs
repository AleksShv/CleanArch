namespace CleanArch.UseCases.Common.Models;

public record PaggingDto<TItem>(TItem[] Items, int PageIndex, int PageSize, int Total)
{
    public int TotalPages { get; } = (int)Math.Ceiling(Total / (decimal)PageSize);
}
