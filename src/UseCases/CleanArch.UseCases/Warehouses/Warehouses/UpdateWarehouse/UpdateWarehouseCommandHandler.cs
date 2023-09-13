using AutoMapper;
using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using CleanArch.UseCases.Warehouses.Exception;

namespace CleanArch.UseCases.Warehouses.Warehouses.UpdateWarehouse;

internal class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateWarehouseCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await _context.Warehouses
            .FindByIdAsync(request.WarehouseId, cancellationToken)
            ?? throw new WarehouseNotFoundExceptions(request.WarehouseId);

        _mapper.Map(request, warehouse);

        await _context.SaveChangesAsync(cancellationToken);

        return warehouse.Id;
    }
}
