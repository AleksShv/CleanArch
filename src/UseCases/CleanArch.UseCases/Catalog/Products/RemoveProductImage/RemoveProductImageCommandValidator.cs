using FluentValidation;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Catalog.Utils;

namespace CleanArch.UseCases.Catalog.Products.RemoveProductImage;

internal class RemoveProductImageCommandValidator : AbstractValidator<RemoveProductImageCommand>
{
    public RemoveProductImageCommandValidator(ProductImageAccessValidator<RemoveProductImageCommand> accessValidator, IApplicationDbContext context)
    {
        RuleFor(x => x.ImageId)
            .ProductImageExist(context)
            .SetAsyncValidator(accessValidator);
    }
}
