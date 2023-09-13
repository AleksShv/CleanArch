namespace CleanArch.Infrastructure.Implementations.UserProvider.Options;

public class UserSettings
{
    public string IdClaimName { get; set; } = default!;
    public string RolesClaimName { get; set; } = default!;
    public string UsernameClaimName { get; set; } = default!;
    public string EmailClaimName { get; set; } = default!;

}
