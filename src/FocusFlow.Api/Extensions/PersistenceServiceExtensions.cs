using FocusFlow.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Extensions;

public static class PersistenceServiceExtensions
{
    public static void AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FocusFlowDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}