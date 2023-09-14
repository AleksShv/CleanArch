using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;

namespace CleanArch.UseCases.Purchasing.Vendors.GetVendorsList;

internal class GetVendorsListQueryHandler : IRequestHandler<GetVendorsListQuery, VendorListItemDto[]>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVendorsListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<VendorListItemDto[]> Handle(GetVendorsListQuery request, CancellationToken cancellationToken)
        => _context.Vendors
            .ProjectTo<VendorListItemDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
}
