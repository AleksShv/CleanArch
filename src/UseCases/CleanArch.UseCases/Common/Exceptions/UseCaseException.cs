namespace CleanArch.UseCases.Common.Exceptions;

public abstract class UseCaseException : Exception
{
    public UseCaseException(string message) 
        : base(message)
    { }

    public UseCaseException(string message, params object?[] args)
        : base(string.Format(message, args))
    { }
}
