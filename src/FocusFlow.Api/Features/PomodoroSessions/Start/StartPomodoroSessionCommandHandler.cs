using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Domain.Enums;
using FocusFlow.Api.Domain.Services;
using FocusFlow.Api.Features.PomodoroSessions.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.PomodoroSessions.Start;

public sealed class StartPomodoroSessionCommandHandler(
    FocusFlowDbContext dbContext,
    IPomodoroSessionBusinessRules rules)
    : IRequestHandler<StartPomodoroSessionCommandRequest, StartPomodoroSessionCommandResponse>
{
    public async Task<StartPomodoroSessionCommandResponse> Handle(
        StartPomodoroSessionCommandRequest request,
        CancellationToken cancellationToken)
    {
        await rules.ClientIdMustBeUniqueAsync(request.UserId, request.ClientId, cancellationToken);
        await rules.UserMustNotHaveRunningSessionAsync(request.UserId, cancellationToken);
        await rules.TaskItemMustBelongToUserAsync(request.UserId, request.TaskItemId, cancellationToken);

        var settings = await dbContext.UserPomodoroSettings
            .FirstOrDefaultAsync(settings => settings.UserId == request.UserId, cancellationToken);

        var durationMinutes = PomodoroSessionDurationResolver.Resolve(request.Type, settings);
        var now = DateTime.UtcNow;

        var pomodoroSession = new PomodoroSessionEntity
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            ClientId = request.ClientId,
            TaskItemId = request.TaskItemId,
            Type = request.Type,
            Status = PomodoroSessionStatus.Running,
            StartedAtUtc = now,
            DurationMinutes = durationMinutes
        };

        await dbContext.PomodoroSessions.AddAsync(pomodoroSession, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new StartPomodoroSessionCommandResponse
        {
            Id = pomodoroSession.Id,
            ClientId = pomodoroSession.ClientId,
            TaskItemId = pomodoroSession.TaskItemId,
            Type = pomodoroSession.Type,
            Status = pomodoroSession.Status,
            StartedAtUtc = pomodoroSession.StartedAtUtc,
            DurationMinutes = pomodoroSession.DurationMinutes
        };
    }
}