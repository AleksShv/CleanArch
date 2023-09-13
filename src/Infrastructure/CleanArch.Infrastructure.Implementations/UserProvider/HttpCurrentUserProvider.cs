using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using CleanArch.Infrastructure.Implementations.UserProvider.Options;
using CleanArch.Infrastructure.Contracts.UserProvider;
using CleanArch.Utils;

namespace CleanArch.Infrastructure.Implementations.UserProvider;

internal class HttpCurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _accessor;
    private readonly UserSettings _userSettings;

    public HttpCurrentUserProvider(IHttpContextAccessor accessor, IOptions<UserSettings> userSettings)
    {
        _accessor = accessor;
        _userSettings = userSettings.Value;
    }

    public T GetUserId<T>(IFormatProvider? formatProvider = null) where T : IParsable<T>
    {
        if (!T.TryParse(GetUserId(), formatProvider, out T? result))
        {
            throw new UnauthorizedAccessException();
        }

        return result;
    }

    public string GetUserId()
        => FindValueByClaim(_userSettings.IdClaimName);

    public T GetRoles<T>() where T : struct, Enum
        => EnumHelper.FromString<T>(GetRoles(), separator: ',');

    public string GetRoles()
        => FindValueByClaim(_userSettings.RolesClaimName);

    public string GetName()
        => FindValueByClaim(_userSettings.IdClaimName);

    public string GetEmail()
        => FindValueByClaim(_userSettings.EmailClaimName);

    private string FindValueByClaim(string claim)
        => _accessor.HttpContext?.User.FindFirst(claim)?.Value
            ?? throw new UnauthorizedAccessException();
}
