using FocusFlow.Api.Domain.Enums;
using FocusFlow.Api.Features.PomodoroSessions.DTOs;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.PomodoroSessions.GetRunning;

public sealed class GetRunningPomodoroSessionHandler(FocusFlowDbContext dbContext)
    : IRequestHandler<GetRunningPomodoroSessionQueryRequest, GetRunningPomodoroSessionQueryResponse>
{
    public async Task<GetRunningPomodoroSessionQueryResponse> Handle(GetRunningPomodoroSessionQueryRequest request, CancellationToken cancellationToken)
    {
        var session = await dbContext.PomodoroSessions
            .AsNoTracking()
            .Where(x =>
            x.UserId == request.UserId &&
            x.Status == PomodoroSessionStatus.Running)
            .OrderByDescending(x => x.StartedAtUtc)
            .Select(x => new RunningPomodoroSessionDto
            {
                Id = x.Id,
                ClientId = x.ClientId,
                TaskItemId = x.TaskItemId,
                TaskTitle = x.TaskItem != null ? x.TaskItem.Title : null,
                Type = x.Type,
                StartedAtUtc = x.StartedAtUtc,
                DurationMinutes = x.DurationMinutes

            }).FirstOrDefaultAsync(cancellationToken);

        return new GetRunningPomodoroSessionQueryResponse
        {
            HasRunningSession = session != null,
            Session = session,
        };
    }
}
