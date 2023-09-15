using FluentValidation;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using CleanArch.UseCases.Internal.Utils;

namespace CleanArch.UseCases.Purchasing.Products.UpdateProductPurchasingDetails;

internal class UpdateProductPurchasingDetailsCommandValidator : AbstractValidator<UpdateProductPurchasingDetailsCommand>
{
    public UpdateProductPurchasingDetailsCommandValidator(IApplicationDbContext context, ProductAccessValidator<UpdateProductPurchasingDetailsCommand> accessValidator)
    {
        RuleFor(x => x.ProductId)
            .ContainsIn(context.Products)
                .WithErrorCode("ProductNotFound")
                .WithMessage("Product not found")
            .SetAsyncValidator(accessValidator);

        RuleFor(x => x.VendorId)
            .ContainsIn(context.Products)
                .WithErrorCode("VendorNotFound")
                .WithMessage("Vendor not found");

        RuleFor(x => x.Cost)
            .GreaterThan(decimal.Zero);
    }
}
