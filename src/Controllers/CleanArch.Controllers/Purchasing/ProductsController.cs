using System.Net;

using Microsoft.AspNetCore.Mvc;

using CleanArch.Controllers.Common;
using CleanArch.Entities;
using CleanArch.UseCases.Purchasing.Products.UpdateProductPurchasingDetails;
using CleanArch.UseCases.Purchasing.Products.PurchaseProduct;
using CleanArch.UseCases.Purchasing.Products.GetProductPurchasingDetails;

namespace CleanArch.Controllers.Purchasing;

[RolesAuthorize(UserRole.Admin, UserRole.ProductOwner)]
public class ProductsController : ApiControllerBase
{
    [HttpGet("{id}/purchasing-info")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProductPurchasingDetailsDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProductPurchasingInfoDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        => await Sender.Send(new GetProductPurchasingDetailsQuery(id), cancellationToken) is var response && response is null
            ? NotFound()
            : Ok(response);

    [HttpPut("{id}/purchasing-info")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProductPurchasingInfoAsync(Guid id, UpdateProductPurchasingDetailsCommand request, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(request with { ProductId = id }, cancellationToken));

    [HttpPost("{id}/supplies")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> PurchaseProductAsync(Guid id, PurchaseProductCommand request, CancellationToken cancellationToken = default)
    {
        var response = await Sender.Send(request with { ProductId = id }, cancellationToken);
        return CreatedAtAction(nameof(SuppliesController.GetSupplyDetailsAsync), new { id = response }, response);
    }
}
