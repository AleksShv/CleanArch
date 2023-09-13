using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehousesList;

internal class GetWarehousesListQueryHandler : IRequestHandler<GetWarehousesListQuery, WarehouseListItemDto[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWarehousesListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WarehouseListItemDto[]> Handle(GetWarehousesListQuery request, CancellationToken cancellationToken)
        => await _context.Warehouses
            .ProjectTo<WarehouseListItemDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
}
