using Microsoft.AspNetCore.Mvc;
using CleanArch.Controllers.Common;

namespace CleanArch.Controllers.Ordering;

public class OrdersController : ApiControllerBase
{
    [HttpPost("ceckout")]
    public Task<IActionResult> CheckoutBasketAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public Task<IActionResult> GetOrderDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}/accept")]
    public Task<IActionResult> AcceptOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}/cancel")]
    public Task<IActionResult> CancelOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
