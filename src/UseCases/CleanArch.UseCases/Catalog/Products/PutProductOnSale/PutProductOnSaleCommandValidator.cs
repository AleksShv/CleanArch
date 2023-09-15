using FluentValidation;
using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Internal.Utils;

namespace CleanArch.UseCases.Catalog.Products.PutProductOnSale;

internal class PutProductOnSaleCommandValidator : AbstractValidator<PutProductOnSaleCommand>
{
    public PutProductOnSaleCommandValidator(ProductAccessValidator<PutProductOnSaleCommand> accessValidator, IApplicationDbContext context)
    {
        RuleFor(x => x.ProductId)
            .ProductExist(context)
            .SetAsyncValidator(accessValidator);
    }
}
