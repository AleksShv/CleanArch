using Microsoft.AspNetCore.Mvc;

using CleanArch.Controllers.Common;
using CleanArch.UseCases.Purchasing.Products.RemoveProductWarehouses;
using CleanArch.UseCases.Purchasing.Products.UpdateProductPurchasingDetails;
using CleanArch.UseCases.Purchasing.Products.PurchaseProduct;
using CleanArch.UseCases.Purchasing.Products.AddProductWarehouses;

namespace CleanArch.Controllers.Purchasing;

public class ProductsController : ApiControllerBase
{
    [HttpGet("{id}/purchasing-info")]
    public Task<IActionResult> GetProductPurchasingInfoDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}/purchasing-info")]
    public async Task<IActionResult> UpdateProductPurchasingInfoAsync(Guid id, UpdateProductPurchasingDetailsCommand request, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(request with { ProductId = id }, cancellationToken));

    [HttpPost("{id}/supplies")]
    public async Task<IActionResult> PurchaseProductAsync(Guid id, PurchaseProductCommand request, CancellationToken cancellationToken = default)
    {
        var response = await Sender.Send(request with { ProductId = id }, cancellationToken);
        return CreatedAtAction(nameof(SuppliesController.GetSupplyDetailsAsync), new { id = response }, response);
    }

    [HttpPut("{id}/warehouses")]
    public async Task<IActionResult> UpdateProductWarehousesAsync(Guid id, AddProductWarehousesCommand request, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(request with { ProductId = id }, cancellationToken));

    [HttpDelete("{id}/warehouses")]
    public async Task<IActionResult> RemoveProductWarehousesAsync(Guid id, RemoveProductWarehousesCommand request, CancellationToken cancellationToken = default)
    {
        await Sender.Send(request with { ProductId = id }, cancellationToken);
        return NoContent();
    }
}
