using Microsoft.AspNetCore.Mvc;
using CleanArch.Controllers.Common;

namespace CleanArch.Controllers.Ordering;

public class BasketsController : ApiControllerBase
{
    [HttpPost]
    public Task<IActionResult> AddBasketAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public Task<IActionResult> GetBasketDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}/items")]
    public Task<IActionResult> AddItemsToBasketAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}/items")]
    public Task<IActionResult> RemoveItemsFromBasketAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
