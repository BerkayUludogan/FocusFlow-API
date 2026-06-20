using System.Net;

namespace FocusFlow.Api.Shared.Exceptions;

public sealed class RequestValidationException : BaseException
{
    public RequestValidationException(List<string> errors)
        : base(errors, (int)HttpStatusCode.BadRequest)
    {
    }
}