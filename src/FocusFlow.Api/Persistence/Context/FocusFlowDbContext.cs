using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FocusFlow.Api.Persistence.Context;

public sealed class FocusFlowDbContext(DbContextOptions<FocusFlowDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<TaskItemEntity> TaskItems => Set<TaskItemEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker
            .Entries<BaseEntity>();

        foreach (var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedAtUtc = DateTime.UtcNow,
                EntityState.Modified => data.Entity.ModifiedAtUtc = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}