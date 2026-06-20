using System.Net;

namespace FocusFlow.Api.Common.Exceptions;

public sealed class BusinessRuleException : BaseException
{
    public BusinessRuleException(string message) : base(message)
    {

    }
    public override int StatusCode => (int)HttpStatusCode.Conflict;
}
