using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.Api.Persistence.Configurations;

public sealed class PomodoroSessionEntityConfiguration : BaseEntityConfiguration<PomodoroSessionEntity>
{
    public override void Configure(EntityTypeBuilder<PomodoroSessionEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("pomodoro_sessions");

        builder.Property(session => session.UserId)
            .IsRequired();

        builder.Property(session => session.TaskItemId)
            .IsRequired(false);

        builder.Property(session => session.ClientId)
            .IsRequired();

        builder.Property(session => session.Type)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(session => session.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(session => session.StartedAtUtc)
            .IsRequired();

        builder.Property(session => session.EndedAtUtc)
            .IsRequired(false);

        builder.Property(session => session.DurationMinutes)
            .IsRequired();

        builder.HasOne(session => session.User)
            .WithMany()
            .HasForeignKey(session => session.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(session => session.TaskItem)
            .WithMany()
            .HasForeignKey(session => session.TaskItemId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(session => new { session.UserId, session.ClientId })
            .IsUnique();

        builder.HasQueryFilter(session => !session.IsDeleted);

    }
}
