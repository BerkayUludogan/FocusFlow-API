using FocusFlow.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Persistence;

public sealed class FocusFlowDbContext(DbContextOptions<FocusFlowDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
}