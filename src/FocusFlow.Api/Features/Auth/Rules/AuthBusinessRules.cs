using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using FocusFlow.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using FocusFlow.Api.Domain.Entities;

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

        public void PasswordMustBeValid(bool isPasswordValid)
        {
            if (!isPasswordValid)
                throw new BusinessRuleException(AuthErrors.InvalidCredentials);
        }

        public UserEntity RefreshTokenMustExist(UserEntity? user)
        {
            if (user is null)
                throw new BusinessRuleException(AuthErrors.RefreshTokenNotFound);
            return user;
        }

        public void RefreshTokenMustNotBeExpired(UserEntity user)
        {
            if (user.RefreshTokenExpiresAtUtc is null || user.RefreshTokenExpiresAtUtc <= DateTime.UtcNow)
                throw new BusinessRuleException(AuthErrors.RefreshTokenExpired);
        }

        public void UserEmailMustBeVerified(UserEntity user)
        {
            if (!user.IsEmailVerified)
                throw new BusinessRuleException(AuthErrors.EmailNotVerified);
        }

        public void UserMustBeActive(UserEntity user)
        {
            if (!user.IsActive)
                throw new BusinessRuleException(AuthErrors.UserNotActive);
        }

        public UserEntity UserMustExist(UserEntity? user)
        {
            if (user is null)
                throw new BusinessRuleException(AuthErrors.UserNotFound);

            return user;
        }
    }
}