namespace CleanArch.UseCases.Auth.Login;

public record LoginResultDto(string AccessToken, string RefreshToken, long Expires);
