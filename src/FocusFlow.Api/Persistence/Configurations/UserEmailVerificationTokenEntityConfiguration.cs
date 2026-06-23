using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.Api.Persistence.Configurations;

public sealed class UserEmailVerificationTokenEntityConfiguration : BaseEntityConfiguration<UserEmailVerificationTokenEntity>
{
    public override void Configure(EntityTypeBuilder<UserEmailVerificationTokenEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("user_email_verification_codes");

        builder.Property(token => token.UserId)
            .IsRequired();

        builder.Property(token => token.CodeHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(token => token.ExpiresAtUtc)
            .IsRequired();

        builder.Property(token => token.UsedAtUtc)
            .IsRequired(false);

        builder.HasOne(token => token.User)
            .WithMany()
            .HasForeignKey(token => token.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(token => token.UserId);
        builder.HasIndex(token => new { token.UserId, token.CodeHash });
        builder.HasQueryFilter(token => !token.IsDeleted);   }
}