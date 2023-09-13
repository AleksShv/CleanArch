using FluentValidation;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Purchasing.Products.AddProductWarehouses;

internal class AddProductWarehousesCommandValidator : AbstractValidator<AddProductWarehousesCommand>
{
    public AddProductWarehousesCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.ProductId)
            .ContainsIn(context.Products)
                .WithErrorCode("ProductNotFound")
                .WithMessage("Product not found");

        RuleForEach(x => x.WarehousesIds)
            .ContainsIn(context.Warehouses)
                .WithErrorCode("ProductNotFound")
                .WithMessage("Product not found"); ;
    }
}
