using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Internal.Exceptions;

public class UserNotFoundException : UseCaseException
{
    public UserNotFoundException(string email)
        : base("User with email: \"{0}\" not found.", email)
    { }
}
