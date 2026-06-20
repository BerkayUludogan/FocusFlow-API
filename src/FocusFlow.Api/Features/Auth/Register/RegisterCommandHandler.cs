using FluentValidation;
using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore; 
using MediatR;

namespace FocusFlow.Api.Features.Auth.Register;

public sealed class RegisterCommandHandler(FocusFlowDbContext dbContext,
    IValidator<RegisterCommandRequest> validator)
    : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
{
    public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(" ", validationResult.Errors.Select(error => error.ErrorMessage));
            throw new ValidationException(errors);
        }

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var emailExists = await dbContext.Users
            .AnyAsync(user => user.Email == normalizedEmail, cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException("Email is already registered.");
        }

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = normalizedEmail,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            DisplayName = request.DisplayName.Trim(),
            IsEmailVerified = false,
            IsActive = true
        };

        dbContext.Users.Add(user);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterCommandResponse
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            CreatedAtUtc = user.CreatedAtUtc
        };
    }
}
