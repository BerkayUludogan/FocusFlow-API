using System.Net;

namespace FocusFlow.Api.Shared.Exceptions;

public sealed class UnauthorizedException : BaseException
{
    public UnauthorizedException(string messageKey)
        : base(messageKey, (int)HttpStatusCode.Unauthorized)
    {
    }
}