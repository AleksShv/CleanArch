using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Auth.Login;

public class UserNotFoundException : UseCaseException
{
    public UserNotFoundException(string email)
        : base("User with email: \"{0}\" not found.", email)
    { }
}
