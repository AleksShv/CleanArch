using FluentValidation;

using CleanArch.UseCases.Catalog.Utils;
using CleanArch.DataAccess.Contracts;

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
