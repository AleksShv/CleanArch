using System.Net;

using Microsoft.AspNetCore.Mvc;

using CleanArch.Controllers.Catalog.Products.Requests;
using CleanArch.Entities;
using CleanArch.Controllers.Common;
using CleanArch.UseCases.Catalog.Products.UpdateProductImageOrder;
using CleanArch.UseCases.Catalog.Products.GetProductImage;
using CleanArch.UseCases.Catalog.Products.RemoveProductImage;

namespace CleanArch.Controllers.Catalog.Products.Controllers;

[Route("api/product-images")]
public sealed class ProductImagesController : ApiControllerBase
{
    [HttpGet("{id}")]
    [Produces("image/png", "image/jpg", "image/jpeg")]
    public async Task<IActionResult> GetProductImageAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetProductImageQuery(id);
        var productImage = await Sender.Send(query, cancellationToken);

        if (!ContentTypeProvider.TryGetContentType(productImage.FileName, out var contentType))
        {
            contentType = DefaultContentType;
        }

        return File(
            fileStream: productImage.Contenntt,
            contentType: contentType,
            fileDownloadName: productImage.FileName);
    }

    [HttpDelete("{id}")]
    [RolesAuthorize(UserRole.Admin, UserRole.ProductOwner)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> RemoveProductImageAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new RemoveProductImageCommand(id);
        await Sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id}/order")]
    [RolesAuthorize(UserRole.Admin, UserRole.ProductOwner)]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProductImageOrderAsync(Guid id, UpdateProductImageOrderRequest request, CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<UpdateProductImageOrderCommand>(request with { ImageId = id });
        var response = await Sender.Send(command, cancellationToken);
        return Ok(response);
    }
}