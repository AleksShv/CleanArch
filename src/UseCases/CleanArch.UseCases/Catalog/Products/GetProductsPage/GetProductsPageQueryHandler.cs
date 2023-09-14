using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Models;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Catalog.Products.GetProductsPage;

internal sealed class GetProductsPageQueryHandler : IRequestHandler<GetProductsPageQuery, PaggingDto<ProductPaggingItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsPageQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaggingDto<ProductPaggingItemDto>> Handle(GetProductsPageQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products
            .Search(p => p.Title, request.SearchString)
            .Where(x => !x.IsDraft)
            .Where(x => x.IsAvailableForSale);

        var total = await query.CountAsync(cancellationToken);

        var products = await query
            .Paging(request.PageIndex, request.PageSize)
            .ProjectTo<ProductPaggingItemDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        var pagging = new PaggingDto<ProductPaggingItemDto>(products, request.PageIndex, request.PageSize, total);

        return pagging;
    }
}