using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Purchasing.Vendors.GetVendorDetails;

internal class GetVendorDetailsQueryHandler : IRequestHandler<GetVendorDetailsQuery, VendorDetailsDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVendorDetailsQueryHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<VendorDetailsDto?> Handle(GetVendorDetailsQuery request, CancellationToken cancellationToken)
        => await _context.Vendors
            .WithId(request.VendorId)
            .ProjectTo<VendorDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}
