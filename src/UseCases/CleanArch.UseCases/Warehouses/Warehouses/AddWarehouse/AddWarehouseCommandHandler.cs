using AutoMapper;
using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;

namespace CleanArch.UseCases.Warehouses.Warehouses.AddWarehouse;

internal class AddWarehouseCommandHandler : IRequestHandler<AddWarehouseCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddWarehouseCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(AddWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = _mapper.Map<Warehouse>(request);

        await _context.Warehouses.AddAsync(warehouse, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return warehouse.Id;
    }
}
