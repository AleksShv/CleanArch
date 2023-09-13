using CleanArch.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Controllers.Purchasing;

public class ProductsController : ApiControllerBase
{
    [HttpGet("{id}/suply-info")]
    public Task<IActionResult> GetProductSuplyInfoDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}/suply-info")]
    public Task<IActionResult> FillProductSuplyInfoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
