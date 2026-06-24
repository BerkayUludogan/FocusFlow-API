using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using System.Security.Claims;

namespace FocusFlow.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId, out var parsedUserId))
            throw new BusinessRuleException(AuthErrors.InvalidToken);

        return parsedUserId;
    }
}
