using FluentValidation;
using FocusFlow.Api.Common.Abstractions.Security;
using FocusFlow.Api.Common.Exceptions;
using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR;

namespace FocusFlow.Api.Features.Auth.Register;

public sealed class RegisterCommandHandler(
    FocusFlowDbContext dbContext,
    IAuthBusinessRules authBusinessRules,
    IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
{

    public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var normalizedDisplayName = request.DisplayName.Trim();

        await authBusinessRules.EmailMustBeUniqueAsync(normalizedEmail, cancellationToken);
        authBusinessRules.DisplayNameMustNotBeReserved(normalizedDisplayName);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = normalizedEmail,
            PasswordHash = passwordHasher.Hash(request.Password),
            DisplayName = normalizedDisplayName,
            IsEmailVerified = false,
            IsActive = true
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterCommandResponse
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = normalizedDisplayName,
            CreatedAtUtc = user.CreatedAtUtc
        };
    }
}
