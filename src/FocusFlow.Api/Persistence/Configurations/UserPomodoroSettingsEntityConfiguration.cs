using FocusFlow.Api.Domain.Constants;
using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.Api.Persistence.Configurations;

public sealed class UserPomodoroSettingsEntityConfiguration : BaseEntityConfiguration<UserPomodoroSettingsEntity>
{
    public override void Configure(EntityTypeBuilder<UserPomodoroSettingsEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("user_pomodoro_settings");

        builder.Property(settings => settings.UserId)
            .IsRequired();

        builder.Property(settings => settings.FocusDurationMinutes)
            .IsRequired()
            .HasDefaultValue(PomodoroSettingsDefaults.FocusDurationMinutes);

        builder.Property(settings => settings.ShortBreakDurationMinutes)
            .IsRequired()
            .HasDefaultValue(PomodoroSettingsDefaults.ShortBreakDurationMinutes);

        builder.Property(settings => settings.LongBreakDurationMinutes)
            .IsRequired()
            .HasDefaultValue(PomodoroSettingsDefaults.LongBreakDurationMinutes);

        builder.Property(settings => settings.LongBreakInterval)
            .IsRequired()
            .HasDefaultValue(PomodoroSettingsDefaults.LongBreakInterval);

        builder.Property(settings => settings.DailyFocusGoalMinutes)
            .IsRequired()
            .HasDefaultValue(PomodoroSettingsDefaults.DailyFocusGoalMinutes);

        builder.Property(settings => settings.AutoStartBreaks)
            .IsRequired()
            .HasDefaultValue(PomodoroSettingsDefaults.AutoStartBreaks);

        builder.Property(settings => settings.AutoStartPomodoros)
            .IsRequired()
            .HasDefaultValue(PomodoroSettingsDefaults.AutoStartPomodoros);

        builder.HasOne(settings => settings.User)
            .WithOne()
            .HasForeignKey<UserPomodoroSettingsEntity>(settings => settings.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(settings => settings.UserId)
            .IsUnique();

        builder.HasQueryFilter(settings => !settings.IsDeleted);
    }
}