using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace infrastrucrue.Database;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString, bool includeHealthCheck = true)
    {
        services.AddDbContext<ThanachartDbContext>(
            opt => opt.UseNpgsql(connectionString, opt => opt.MigrationsAssembly("migrations"))
                .UseSnakeCaseNamingConvention());
        if (includeHealthCheck)
            services.AddHealthChecks().AddNpgSql(connectionString);
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        return services;
    }

    public static async Task MigrateDatabase(this WebApplication app)
    {
        var dbContext = app.Services.GetService<ThanachartDbContext>()!;
        await dbContext.Database.MigrateAsync();
    }
}
