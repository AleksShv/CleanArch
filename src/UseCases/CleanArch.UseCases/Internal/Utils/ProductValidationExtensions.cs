using FluentValidation;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Internal.Utils;

internal static class ProductValidationExtensions
{
    public static IRuleBuilderOptions<T, Guid> ProductExist<T>(this IRuleBuilder<T, Guid> ruleBuilder, IApplicationDbContext context)
        => ruleBuilder
            .MustAsync(context.Products.ContainsWithIdAsync)
                .WithErrorCode("ProductNotFound")
                .WithMessage("Product by id {PropertyValue} not found");

    public static IRuleBuilderOptions<T, Guid> ProductImageExist<T>(this IRuleBuilder<T, Guid> ruleBuilder, IApplicationDbContext context)
        => ruleBuilder
            .MustAsync(context.ProductImages.ContainsWithIdAsync)
                .WithErrorCode("ProductImageNotFound")
                .WithMessage("Product image by id {PropertyValue} not found");
}
