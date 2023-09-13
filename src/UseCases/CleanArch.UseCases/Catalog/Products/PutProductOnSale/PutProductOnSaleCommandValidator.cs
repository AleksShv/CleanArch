using FluentValidation;

using CleanArch.UseCases.Catalog.Utils;
using CleanArch.DataAccess.Contracts;

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
