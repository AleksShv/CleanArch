using FluentValidation;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using CleanArch.UseCases.Internal.Utils;

namespace CleanArch.UseCases.Purchasing.Products.PurchaseProduct;

internal class PurchaseProductCommandValidator : AbstractValidator<PurchaseProductCommand>
{
    public PurchaseProductCommandValidator(ProductAccessValidator<PurchaseProductCommand> accessValidator, IApplicationDbContext context)
    {
        RuleFor(x => x.ProductId)
            .SetAsyncValidator(accessValidator);

        RuleFor(x => x.WarehouseId)
            .ContainsIn(context.Warehouses)
                .WithErrorCode("WarehouseNotFound")
                .WithMessage("Warehouse not found");
    }
}
