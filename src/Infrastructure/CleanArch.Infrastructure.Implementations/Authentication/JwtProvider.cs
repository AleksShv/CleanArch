using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CleanArch.Infrastructure.Implementations.Authentication.Options;
using CleanArch.Infrastructure.Contracts.Authentication;

namespace CleanArch.Infrastructure.Implementations.Authentication;

internal class JwtProvider : IJwtProvider
{
    private readonly IOptions<JwtSettings> _options;

    public JwtProvider(IOptions<JwtSettings> options)
    {
        _options = options;
    }

    public JwtDto Generate(UserDto data)
    {
        var jwtSettings = _options.Value;

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, data.Id),
            new Claim(ClaimTypes.Email, data.Email),
            new Claim(ClaimTypes.Name, data.Name),
            new Claim(ClaimTypes.Role, data.Roles),
        };

        var expires = DateTime.UtcNow.AddMinutes(jwtSettings.LifeTime);
        var jwt = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: new SigningCredentials(
                key: jwtSettings.SecurityKey,
                algorithm: SecurityAlgorithms.HmacSha256));

        var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new(encodeJwt, expires);
    }
}
