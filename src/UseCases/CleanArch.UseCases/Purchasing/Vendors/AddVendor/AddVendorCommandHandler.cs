using AutoMapper;
using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;

namespace CleanArch.UseCases.Purchasing.Vendors.AddVendor;

internal class AddVendorCommandHandler : IRequestHandler<AddVendorCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddVendorCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(AddVendorCommand request, CancellationToken cancellationToken)
    {
        var vendor = _mapper.Map<Vendor>(request);

        await _context.Vendors.AddAsync(vendor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return vendor.Id;
    }
}
