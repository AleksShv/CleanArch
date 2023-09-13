using System.Security.Cryptography;
using CleanArch.Infrastructure.Contracts.Authentication;

namespace CleanArch.Infrastructure.Implementations.Authentication;

internal class RefreshTokenProvider : IRefreshTokenProvider
{
    public string Generate()
    {
        var bytes = new byte[64];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }
}
