using AutoMapper;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.DomainServices.Warehouse.Utils;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseProducts;

internal class GetWarehouseProductsQueryHandler : IRequestHandler<GetWarehouseProductsQuery, ProductDetailsDto[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWarehouseProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDetailsDto[]> Handle(GetWarehouseProductsQuery request, CancellationToken cancellationToken)
        => await _context.Products
            .DecompileAsync()
            .Where(p => p.FormWarehouse(request.WarehouseId))
            .ProjectTo<ProductDetailsDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
}
