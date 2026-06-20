using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using FocusFlow.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Auth.Rules
{
    public sealed class AuthBusinessRules(FocusFlowDbContext context) : IAuthBusinessRules
    {
        private static readonly HashSet<string> ReservedDisplayNames = new(StringComparer.OrdinalIgnoreCase)
         {
            "admin","administrator","root","system","support","focusflow"
         }; 
        public void DisplayNameMustNotBeReserved(string displayName)
        {
            var normalizedDisplayName = displayName.Trim();

            if (ReservedDisplayNames.Contains(normalizedDisplayName))
                throw new BusinessRuleException(AuthErrors.DisplayNameReserved);
        }

        public async Task EmailMustBeUniqueAsync(string email, CancellationToken cancellationToken)
        {
            var emailExist = await context.Users
                .AnyAsync(x => x.Email == email, cancellationToken);

            if (emailExist)
                throw new BusinessRuleException(AuthErrors.EmailAlreadyRegistered);
        }
    }
}