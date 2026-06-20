namespace FocusFlow.Api.Shared.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(string messageKey, int statusCode)
        : this(new List<string> { ErrorMessageResolver.Get(messageKey) }, statusCode)
    {
    }

    protected BaseException(List<string> errors, int statusCode)
        : base(errors.FirstOrDefault() ?? "Application error")
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    public int StatusCode { get; }

    public IReadOnlyList<string> Errors { get; }
}