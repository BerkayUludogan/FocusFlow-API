using System.Net;

namespace FocusFlow.Api.Shared.Exceptions;

public sealed class BusinessRuleException : BaseException
{
    public BusinessRuleException(string messageKey)
        : base(messageKey, (int)HttpStatusCode.UnprocessableEntity)
    {
    }
}