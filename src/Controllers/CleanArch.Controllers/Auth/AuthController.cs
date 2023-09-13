using Microsoft.AspNetCore.Mvc;

using CleanArch.Controllers.Common;
using CleanArch.UseCases.Auth.Login;
using CleanArch.UseCases.Auth.Register;

namespace CleanArch.Controllers.Auth;

public class AuthController : ApiControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginCommand request, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(request, cancellationToken));

    [HttpPost("register")]    
    public async Task<IActionResult> RegisterAsync(RegisterCommand request, CancellationToken cancellationToken = default)
        => Ok(await Sender.Send(request, cancellationToken));
}