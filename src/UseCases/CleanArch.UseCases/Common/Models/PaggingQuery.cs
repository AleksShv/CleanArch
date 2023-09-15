using MediatR;

namespace CleanArch.UseCases.Common.Models;

public abstract record PaggingQuery<TPagingItem>(
    int PageIndex,
    int PageSize,
    int SearchString,
    string SortBy,
    SortDirection SortDirection) : IRequest<PaggingDto<TPagingItem>>;

public enum SortDirection
{
    Ascending,
    Descending
}