using FocusFlow.Api.Domain.Constants.FieldLengths;
using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.Api.Persistence.Configurations
{
    public class UserEntityConfiguration : BaseEntityConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("users");

            builder.Property(user => user.Email)
           .IsRequired()
           .HasMaxLength(UserFieldLengths.Email);

            builder.HasIndex(user => user.Email)
                .IsUnique();

            builder.Property(user => user.PasswordHash)
                .IsRequired()
                .HasMaxLength(UserFieldLengths.PasswordHash);

            builder.Property(user => user.DisplayName)
                .IsRequired()
                .HasMaxLength(UserFieldLengths.DisplayName);

            builder.Property(user => user.RefreshToken)
                .HasMaxLength(UserFieldLengths.RefreshToken);

            builder.Property(user => user.RefreshTokenExpiresAtUtc)
                .IsRequired(false);

            builder.Property(user => user.IsEmailVerified)
                .HasDefaultValue(false);

            builder.Property(user => user.IsActive)
                .HasDefaultValue(true);

            builder.Property(user => user.LastLoginAtUtc)
                .IsRequired(false);

            builder.HasQueryFilter(user => !user.IsDeleted);
        }
    }
}