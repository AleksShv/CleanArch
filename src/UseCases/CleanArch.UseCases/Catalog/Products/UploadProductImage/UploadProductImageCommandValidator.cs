using FluentValidation;

using CleanArch.DomainServices.Catalog.Consts;
using CleanArch.DomainServices.Catalog.Services;
using CleanArch.UseCases.Catalog.Utils;
using CleanArch.DataAccess.Contracts;

namespace CleanArch.UseCases.Catalog.Products.UploadProductImage;

internal class UploadProductImageCommandValidator : AbstractValidator<UploadProductImageCommand>
{
    public UploadProductImageCommandValidator(ProductAccessValidator<UploadProductImageCommand> accessValidator, IApplicationDbContext context)
    {
        RuleFor(x => x.ProductId)
            .ProductExist(context)
            .SetAsyncValidator(accessValidator);

        RuleFor(x => x.FileName)
            .Must(ProductImageManager.IsValidImageExtension)
                .WithErrorCode("InvalidImageFormat")
                .WithMessage("Invalid image format");

        RuleFor(x => x.Source.Length)
            .LessThanOrEqualTo(ProductImageConsts.MaxSize)
                .WithErrorCode("InvalidImageSize")
                .WithMessage("Invalid image size");
    }
}