using AutoMapper;
using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using CleanArch.UseCases.Purchasing.Exceptions;

namespace CleanArch.UseCases.Purchasing.Vendors.UpdateVendor;

internal class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateVendorCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        var vendor = await _context.Vendors
            .FindByIdOrDefaultAsync(request.Id, cancellationToken)
            ?? throw new VendorNotFoundException(request.Id);

        _mapper.Map(request, vendor);

        await _context.SaveChangesAsync(cancellationToken);

        return vendor.Id;
    }
}
