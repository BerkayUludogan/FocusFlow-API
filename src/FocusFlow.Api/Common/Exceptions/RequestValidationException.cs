using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FocusFlow.Api.Common.Exceptions;

public sealed class RequestValidationException : BaseException
{
    public RequestValidationException(IReadOnlyDictionary<string, string[]> errors)
       : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;
}

