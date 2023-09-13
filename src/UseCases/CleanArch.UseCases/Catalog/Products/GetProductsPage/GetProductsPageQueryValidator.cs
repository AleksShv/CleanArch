using FluentValidation;

namespace CleanArch.UseCases.Catalog.Products.GetProductsPage;

internal class GetProductsPageQueryValidator : AbstractValidator<GetProductsPageQuery>
{
    public GetProductsPageQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(1);
    }
}
