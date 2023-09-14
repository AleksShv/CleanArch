using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.UseCases.Purchasing.Supplies.GetSupplyDetails;

internal class GetSupplyDetailsCommandHandler : IRequestHandler<GetSupplyDetailsCommand, SupplyDetailsDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSupplyDetailsCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SupplyDetailsDto?> Handle(GetSupplyDetailsCommand request, CancellationToken cancellationToken)
        => await _context.Supplies
            .WithId(request.SupplyId)
            .ProjectTo<SupplyDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}
