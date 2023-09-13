using FluentValidation;
using CleanArch.DomainServices.Catalog.Consts;

namespace CleanArch.UseCases.Catalog.Products.AddProduct;
internal sealed class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(ProductConsts.TitleLength.Min)
            .MaximumLength(ProductConsts.TitleLength.Max);

        RuleFor(x => x.Description)
            .MinimumLength(ProductConsts.DescriptionLength.Min)
            .MaximumLength(ProductConsts.DescriptionLength.Max);

        RuleFor(x => x.Price)
            .LessThan(ProductConsts.Price.Max)
            .GreaterThan(ProductConsts.Price.Min)
            .PrecisionScale(6, ProductConsts.Price.Max.Scale, ignoreTrailingZeros: false);
    }
}