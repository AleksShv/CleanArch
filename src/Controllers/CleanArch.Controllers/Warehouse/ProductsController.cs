using CleanArch.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Controllers.Warehouse;

public class ProductsController : ApiControllerBase
{
    [HttpPut("{id}/transport")]
    public Task<IActionResult> TransportProductAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
