using FluentValidation;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Warehouses.Warehouses.UpdateWarehouse;

internal class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    public UpdateWarehouseCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.WarehouseId)
            .ContainsIn(context.Warehouses)
                .WithErrorCode("WarehouseNotFound")
                .WithMessage("Warehouse not found");
    }
}
