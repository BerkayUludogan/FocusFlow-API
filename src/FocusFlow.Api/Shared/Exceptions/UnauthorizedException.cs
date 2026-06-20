namespace FocusFlow.Api.Shared.Exceptions;

public sealed class UnauthorizedException : BaseException
{
    public UnauthorizedException(string messageKey)
        : base(messageKey, 401)
    {
    }
}
