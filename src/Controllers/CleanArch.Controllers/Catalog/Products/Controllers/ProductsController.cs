using System.Net;

using Microsoft.AspNetCore.Mvc;

using CleanArch.Controllers.Catalog.Products.Requests;
using CleanArch.Controllers.Catalog.Products.Responses;
using CleanArch.Entities;
using CleanArch.Controllers.Common;
using CleanArch.UseCases.Catalog.Products.AddProduct;
using CleanArch.UseCases.Catalog.Products.UpdateProduct;
using CleanArch.UseCases.Catalog.Products.GetProductsPage;
using CleanArch.UseCases.Catalog.Products.GetProductDetails;
using CleanArch.UseCases.Catalog.Products.RemoveProduct;
using CleanArch.UseCases.Catalog.Products.UploadProductImage;
using CleanArch.UseCases.Catalog.Products.PutProductOnSale;

namespace CleanArch.Controllers.Catalog.Products.Controllers;

public sealed class ProductsController : ApiControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(PaggingResponse<ProductPaggingItemResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductPageAsync([FromQuery] GetProductPageRequest request, CancellationToken cancellationToken = default)
    {
        var query = Mapper.Map<GetProductsPageQuery>(request);
        var productsPage = await Sender.Send(query, cancellationToken);
        var response = Mapper.Map<PaggingResponse<ProductPaggingItemResponse>>(productsPage);
        return Ok(response);
    }

    [HttpPost]
    [RolesAuthorize(UserRole.ProductOwner)]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddProductAsync(AddProductRequest request, CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<AddProductCommand>(request);
        var id = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetProductDetailsAsync), new { id }, id);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDetailsResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetProductDetailsQuery(id);
        var details = await Sender.Send(query, cancellationToken);

        if (details is null)
        {
            return NotFound();
        }

        var response = Mapper.Map<ProductDetailsResponse>(details);

        return Ok(response);
    }

    [HttpPut("{id}")]
    [RolesAuthorize(UserRole.Admin, UserRole.ProductOwner)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProductAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<UpdateProductCommand>(request with { ProductId = id });
        await Sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> RemoveProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new RemoveProductCommand(id);
        await Sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id}/images")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UploadProductImageAsync(Guid id, [FromForm] UploadProductImagesRequest request, CancellationToken cancellationToken = default)
    {
        var command = new UploadProductImageCommand(
            ProductId: id,
            Source: request.ImageFile.OpenReadStream(),
            FileName: Path.GetFileName(request.ImageFile.FileName),
            Order: request.Order);

        var imageId = await Sender.Send(command, cancellationToken);

        return Ok(imageId);
    }

    [HttpPut("{id}/sales")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> PutProductOnSaleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new PutProductOnSaleCommand(id);
        await Sender.Send(command, cancellationToken);

        return Ok();
    }
}