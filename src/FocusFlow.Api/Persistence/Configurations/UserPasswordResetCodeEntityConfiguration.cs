using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.Api.Persistence.Configurations;

public sealed class UserPasswordResetCodeEntityConfiguration
    : BaseEntityConfiguration<UserPasswordResetCodeEntity>
{
    public override void Configure(EntityTypeBuilder<UserPasswordResetCodeEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("user_password_reset_codes");

        builder.Property(code => code.UserId)
            .IsRequired();

        builder.Property(code => code.CodeHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(code => code.ExpiresAtUtc)
            .IsRequired();

        builder.Property(code => code.UsedAtUtc)
            .IsRequired(false);

        builder.HasOne(code => code.User)
            .WithMany()
            .HasForeignKey(code => code.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(code => code.UserId);

        builder.HasIndex(code => new { code.UserId, code.CodeHash });

        builder.HasQueryFilter(code => !code.IsDeleted);

    }
}