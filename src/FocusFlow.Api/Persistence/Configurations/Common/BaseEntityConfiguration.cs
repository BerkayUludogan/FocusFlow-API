using FocusFlow.Api.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.Api.Persistence.Configurations.Common
{
    public class BaseEntityConfiguration<TEntity> :
        IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .ValueGeneratedNever();

            builder.Property(entity => entity.CreatedAtUtc)
                .IsRequired();

            builder.Property(entity => entity.ModifiedAtUtc)
                .IsRequired(false);

            builder.Property(entity => entity.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
