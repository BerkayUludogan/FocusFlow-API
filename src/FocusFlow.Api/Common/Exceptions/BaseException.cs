namespace FocusFlow.Api.Common.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(string message) : base(message)
    {

    }
    public abstract int StatusCode { get; }
}
