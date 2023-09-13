using MediatR;

using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Catalog.Products.UploadProductImage;

public record UploadProductImageCommand(
    Guid ProductId,
    Stream Source,
    string FileName,
    int Order) : IRequest<Guid>, IValidatableRequest;