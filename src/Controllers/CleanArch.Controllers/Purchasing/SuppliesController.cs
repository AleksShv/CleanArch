using Microsoft.AspNetCore.Mvc;

using CleanArch.Controllers.Common;

namespace CleanArch.Controllers.Purchasing;

public class SuppliesController : ApiControllerBase
{
    [HttpGet("{id}")]
    public Task<IActionResult> GetSupplyDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    

    [HttpPut("{id}/finish")]
    public Task<IActionResult> FinishSuplyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
