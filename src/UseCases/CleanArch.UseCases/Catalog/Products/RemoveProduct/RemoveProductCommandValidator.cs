using FluentValidation;

using CleanArch.UseCases.Catalog.Utils;
using CleanArch.DataAccess.Contracts;

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
