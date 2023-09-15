using FluentValidation;

using CleanArch.UseCases.Internal.Utils;

namespace CleanArch.UseCases.Purchasing.Products.GetProductPurchasingDetails;

internal class GetProductPurchasingDetailsQueryValidator : AbstractValidator<GetProductPurchasingDetailsQuery>
{
    public GetProductPurchasingDetailsQueryValidator(ProductAccessValidator<GetProductPurchasingDetailsQuery> accessValidator)
    {
        RuleFor(x => x.ProductId)
            .SetAsyncValidator(accessValidator);
    }
}
