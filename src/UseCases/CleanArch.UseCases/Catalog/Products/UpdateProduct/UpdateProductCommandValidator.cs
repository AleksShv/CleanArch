using FluentValidation;

using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Catalog.Products.AddProduct;
using CleanArch.UseCases.Internal.Utils;

namespace CleanArch.UseCases.Catalog.Products.UpdateProduct;

internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(
        ProductAccessValidator<UpdateProductCommand> accessValidator,
        IValidator<AddProductCommand> addValidator, 
        IApplicationDbContext context)
    {
        RuleFor(x => x.ProductId)
            .ProductExist(context)
            .SetAsyncValidator(accessValidator);
        
        Include(addValidator);
    }
}
