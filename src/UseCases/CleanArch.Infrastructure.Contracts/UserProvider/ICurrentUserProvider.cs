namespace CleanArch.Infrastructure.Contracts.UserProvider;

public interface ICurrentUserProvider
{
    string GetUserId();
    T GetUserId<T>(IFormatProvider? formatProvider = null) where T : IParsable<T>;

    string GetRoles();
    TEnum GetRoles<TEnum>() where TEnum : struct, Enum;

    string GetName();
    string GetEmail();
}
