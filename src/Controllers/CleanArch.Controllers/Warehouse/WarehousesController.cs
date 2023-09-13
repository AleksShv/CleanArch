using System.Net;

using Microsoft.AspNetCore.Mvc;

using CleanArch.Controllers.Common;
using CleanArch.Entities;
using CleanArch.UseCases.Warehouses.Warehouses.AddWarehouse;
using CleanArch.UseCases.Warehouses.Warehouses.UpdateWarehouse;
using CleanArch.UseCases.Warehouses.Warehouses.RemoveWarehouse;
using CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseDetails;
using CleanArch.UseCases.Warehouses.Warehouses.GetWarehouseProducts;
using CleanArch.UseCases.Warehouses.Warehouses.GetWarehouses;

namespace CleanArch.Controllers.Warehouse;

[RolesAuthorize(UserRole.Admin)]
public class WarehousesController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(WarehouseListItemDto[]), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetWarehouseListAsync(CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(new GetWarehousesListQuery(), cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddWarehouseAsync(AddWarehouseCommand request, CancellationToken cancellationToken = default)
    {
        var response = await Sender.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetWarehouseDetailsAsync), new { id = response }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WarehouseDetailsDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetWarehouseDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        => await Sender.Send(new GetWarehouseDetailsQuery(id), cancellationToken) is var response && response is null
            ? NotFound()
            : Ok(response);

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateWarehouseAsync(Guid id, UpdateWarehouseCommand request, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(request with { WarehouseId = id }, cancellationToken));

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> RemoveWarehouseAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Sender.Send(new RemoveWarehouseCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id}/products")]
    [ProducesResponseType(typeof(ProductDetailsDto[]), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetWarehouseProductsAsync(Guid id, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(new GetWarehouseProductsQuery(id), cancellationToken));
}
