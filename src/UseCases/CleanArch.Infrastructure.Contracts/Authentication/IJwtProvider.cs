namespace CleanArch.Infrastructure.Contracts.Authentication;

public interface IJwtProvider
{
    JwtDto Generate(UserDto data);
}

public record UserDto(string Id, string Email, string Name, string Roles);
public record JwtDto(string AccessToken, DateTime Expires);