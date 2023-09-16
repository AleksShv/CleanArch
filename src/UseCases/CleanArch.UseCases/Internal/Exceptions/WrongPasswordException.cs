using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Internal.Exceptions;

public class WrongPasswordException : UseCaseException
{
    public WrongPasswordException()
        : base("The password is wrong")
    {
    }
}
