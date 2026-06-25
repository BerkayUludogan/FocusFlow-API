using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.UserPomodoroSettings.Update;

public sealed class UpdateUserPomodoroSettingsCommandHandler(FocusFlowDbContext dbContext) :
    IRequestHandler<UpdateUserPomodoroSettingsCommandRequest, UpdateUserPomodoroSettingsCommandResponse>
{
    public async Task<UpdateUserPomodoroSettingsCommandResponse> Handle(UpdateUserPomodoroSettingsCommandRequest request, CancellationToken cancellationToken)
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
        }

        settings.FocusDurationMinutes = request.FocusDurationMinutes;
        settings.ShortBreakDurationMinutes = request.ShortBreakDurationMinutes;
        settings.LongBreakDurationMinutes = request.LongBreakDurationMinutes;
        settings.LongBreakInterval = request.LongBreakInterval;
        settings.DailyFocusGoalMinutes = request.DailyFocusGoalMinutes;
        settings.AutoStartBreaks = request.AutoStartBreaks;
        settings.AutoStartPomodoros = request.AutoStartPomodoros;
        settings.ModifiedAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateUserPomodoroSettingsCommandResponse
        {
            Id = settings.Id,
            FocusDurationMinutes = settings.FocusDurationMinutes,
            ShortBreakDurationMinutes = settings.ShortBreakDurationMinutes,
            LongBreakDurationMinutes = settings.LongBreakDurationMinutes,
            LongBreakInterval = settings.LongBreakInterval,
            DailyFocusGoalMinutes = settings.DailyFocusGoalMinutes,
            AutoStartBreaks = settings.AutoStartBreaks,
            AutoStartPomodoros = settings.AutoStartPomodoros,
            ModifiedAtUtc = settings.ModifiedAtUtc
        };
    }
}