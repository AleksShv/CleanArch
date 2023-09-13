using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseDetails;

internal class GetWarehouseDetailsQueryHandler : IRequestHandler<GetWarehouseDetailsQuery, WarehouseDetailsDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWarehouseDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<WarehouseDetailsDto?> Handle(GetWarehouseDetailsQuery request, CancellationToken cancellationToken)
        => _context.Warehouses
            .WithId(request.WarehouseId)
            .ProjectTo<WarehouseDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}
