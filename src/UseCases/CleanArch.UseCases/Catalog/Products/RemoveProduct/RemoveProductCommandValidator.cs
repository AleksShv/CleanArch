using FluentValidation;
using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Internal.Utils;

namespace CleanArch.UseCases.Catalog.Products.RemoveProduct;

internal class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
{
    public RemoveProductCommandValidator(ProductAccessValidator<RemoveProductCommand> accessValidator, IApplicationDbContext context)
    {
        RuleFor(x => x.ProductId)
            .ProductExist(context)
            .SetAsyncValidator(accessValidator);
    }
}
