using FocusFlow.Api.Domain.Constants.FieldLengths;
using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.Api.Persistence.Configurations;

public sealed class TaskItemEntityConfiguration : BaseEntityConfiguration<TaskItemEntity>
{
    public override void Configure(EntityTypeBuilder<TaskItemEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("task_items");

        builder.Property(task => task.UserId)
            .IsRequired();

        builder.Property(task => task.ClientId)
            .IsRequired();

        builder.Property(task => task.Title)
            .IsRequired()
            .HasMaxLength(TaskItemFieldLengths.Title);

        builder.Property(task => task.Description)
            .HasMaxLength(TaskItemFieldLengths.Description);

        builder.Property(task => task.IsCompleted)
            .HasDefaultValue(false);

        builder.Property(task => task.CompletedAtUtc)
            .IsRequired(false);

        builder.Property(task => task.DueDateUtc)
            .IsRequired(false);

        builder.Property(task => task.EstimatedPomodoroCount)
            .HasDefaultValue(0);

        builder.Property(task => task.CompletedPomodoroCount)
            .HasDefaultValue(0);

        builder.HasOne(task => task.User)
            .WithMany()
            .HasForeignKey(task => task.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(task => new { task.UserId, task.ClientId })
            .IsUnique();

        builder.HasQueryFilter(task => !task.IsDeleted);
    }
}