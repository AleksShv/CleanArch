using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Auth.Login;

public class WrongPasswordException : UseCaseException
{
    public WrongPasswordException()
        : base("The password is wrong")
    {
    }
}
