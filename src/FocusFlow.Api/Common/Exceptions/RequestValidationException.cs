namespace FocusFlow.Api.Common.Exceptions;

public sealed class RequestValidationException : BaseException
{
    public RequestValidationException(string messageKey)
            : base(new List<string> { messageKey }, 400) { }

    public RequestValidationException(List<string> messageKeys)
        : base(messageKeys, 400) { }
}

