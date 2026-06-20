namespace FocusFlow.Api.Shared.Exceptions;

public abstract class BaseException : Exception
{
    public int StatusCode { get; }
    public List<string> Errors { get; }

    protected BaseException(string messageKey, int statusCode)
        : base(ErrorMessageResolver.Get(messageKey))
    {
        StatusCode = statusCode;
        Errors = new List<string>
            {
                ErrorMessageResolver.Get(messageKey)
            };
    }
    protected BaseException(List<string> messageKeys, int statusCode)
    {
        StatusCode = statusCode;

        Errors = messageKeys?
            .Select(ErrorMessageResolver.Get)
            .ToList()
            ?? new List<string>();
    }
}
