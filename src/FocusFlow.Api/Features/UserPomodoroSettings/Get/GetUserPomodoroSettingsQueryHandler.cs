using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.UserPomodoroSettings.Get;

public sealed class GetUserPomodoroSettingsQueryHandler(FocusFlowDbContext dbContext)
    : IRequestHandler<GetUserPomodoroSettingsQueryRequest, GetUserPomodoroSettingsQueryResponse>
{
    public async Task<GetUserPomodoroSettingsQueryResponse> Handle(GetUserPomodoroSettingsQueryRequest request, CancellationToken cancellationToken)
    {
        var settings = await dbContext.UserPomodoroSettings
            .FirstOrDefaultAsync(settings => settings.UserId == request.UserId, cancellationToken);

        if (settings is null)
        {
            settings = new UserPomodoroSettingsEntity
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId
            };

            await dbContext.UserPomodoroSettings.AddAsync(settings, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return new GetUserPomodoroSettingsQueryResponse
        {
            Id = settings.Id,
            FocusDurationMinutes = settings.FocusDurationMinutes,
            ShortBreakDurationMinutes = settings.ShortBreakDurationMinutes,
            LongBreakDurationMinutes = settings.LongBreakDurationMinutes,
            LongBreakInterval = settings.LongBreakInterval,
            DailyFocusGoalMinutes = settings.DailyFocusGoalMinutes,
            AutoStartBreaks = settings.AutoStartBreaks,
            AutoStartPomodoros = settings.AutoStartPomodoros
        };
    }
}