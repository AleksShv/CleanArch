using System.Net;

using Microsoft.AspNetCore.Mvc;

using CleanArch.Controllers.Common;
using CleanArch.Entities;
using CleanArch.UseCases.Purchasing.Vendors.AddVendor;
using CleanArch.UseCases.Purchasing.Vendors.GetVendorDetails;
using CleanArch.UseCases.Purchasing.Vendors.UpdateVendor;
using CleanArch.UseCases.Purchasing.Vendors.RemoveVendor;
using CleanArch.UseCases.Purchasing.Vendors.GetVendorsList;

namespace CleanArch.Controllers.Purchasing;

[RolesAuthorize(UserRole.Admin)]
public class VendorsController : ApiControllerBase
{
    [HttpGet]
    [RolesAuthorize(UserRole.Admin, UserRole.WarehouseWorker, UserRole.ProductOwner)]
    [ProducesResponseType(typeof(VendorListItemDto[]), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetVendorsListAsync(CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(new GetVendorsListQuery(), cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> AddVendorAsync(AddVendorCommand request, CancellationToken cancellationToken = default)
    {
        var response = await Sender.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetVendorDetailsAsync), new { id = response }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(VendorDetailsDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetVendorDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(new GetVendorDetailsQuery(id), cancellationToken));

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateVendorAsync(Guid id, UpdateVendorCommand request, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(request with { Id = id }, cancellationToken));

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> RemoveVendorAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Sender.Send(new RemoveVendorCommand(id), cancellationToken);
        return NoContent();
    }
}