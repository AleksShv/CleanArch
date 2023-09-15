using FluentValidation;
using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Internal.Utils;

namespace CleanArch.UseCases.Catalog.Products.UpdateProductImageOrder;

internal class UpdateProductImageOrderCommandValidator : AbstractValidator<UpdateProductImageOrderCommand>
{
    public UpdateProductImageOrderCommandValidator(ProductImageAccessValidator<UpdateProductImageOrderCommand> accessValidator, IApplicationDbContext context)
    {
        RuleFor(x => x.ImageId)
            .ProductImageExist(context)
            .SetAsyncValidator(accessValidator);
    }
}
