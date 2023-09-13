using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace CleanArch.Infrastructure.Implementations.Authentication.Options;

public class JwtSettings
{
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Key { get; set; } = default!;
    public int LifeTime { get; set; }

    public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}
