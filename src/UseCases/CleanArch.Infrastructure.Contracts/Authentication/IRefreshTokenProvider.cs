namespace CleanArch.Infrastructure.Contracts.Authentication;

public interface IRefreshTokenProvider
{
    string Generate();
}
