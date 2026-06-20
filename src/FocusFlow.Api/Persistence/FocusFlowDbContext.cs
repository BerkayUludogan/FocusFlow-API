using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Persistence;

public sealed class FocusFlowDbContext(DbContextOptions<FocusFlowDbContext> options) : DbContext(options)
{

}

