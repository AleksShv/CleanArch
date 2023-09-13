using FluentValidation;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseProducts;

internal class GetWarehouseProductsQueryValidator : AbstractValidator<GetWarehouseProductsQuery>
{
    public GetWarehouseProductsQueryValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.WarehouseId)
            .ContainsIn(context.Warehouses);
    }
}
