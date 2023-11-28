using System.Net;

using Microsoft.AspNetCore.Mvc;

using CleanArch.Controllers.Common;
using CleanArch.UseCases.Purchasing.Supplies.CompleteSuply;
using CleanArch.UseCases.Purchasing.Supplies.GetSupplyDetails;

namespace CleanArch.Controllers.Purchasing;

public class SuppliesController : ApiControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(SupplyDetailsDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetSupplyDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        => await Sender.Send(new GetSupplyDetailsCommand(id), cancellationToken) is var response && response is null
            ? NotFound()
            : Ok(response);
    
    [HttpPut("{id}/complete")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> CompleteSupplyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Sender.Send(new CompleteSupplyCommand(id), cancellationToken);
        return NoContent();
    }
}
