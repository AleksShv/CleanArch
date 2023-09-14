using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Purchasing.Products.GetProductPurchasingDetails;

internal class GetProductPurchasingDetailsQueryHandler : IRequestHandler<GetProductPurchasingDetailsQuery, ProductPurchasingDetailsDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductPurchasingDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductPurchasingDetailsDto?> Handle(GetProductPurchasingDetailsQuery request, CancellationToken cancellationToken)
        => await _context.Products
            .WithId(request.ProductId)
            .ProjectTo<ProductPurchasingDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}
