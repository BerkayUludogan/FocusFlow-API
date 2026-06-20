namespace FocusFlow.Api.Common.Exceptions;

public sealed class UnauthorizedException : BaseException
{
    public UnauthorizedException(string messageKey)
        : base(messageKey, 401)
    {
    }
}
