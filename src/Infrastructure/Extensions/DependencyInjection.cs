using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool useSqliteDemo = false
    )
    {
        if (useSqliteDemo)
        {
            // Use SQLite for demo purposes
            services.AddDbContext<TransferMarketDbContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString("SqliteConnection"),
                    b => b.MigrationsAssembly(typeof(TransferMarketDbContext).Assembly.FullName)
                )
            );
        }
        else
        {
            // Use SQL Server for production
            services.AddDbContext<TransferMarketDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(TransferMarketDbContext).Assembly.FullName)
                )
            );
        }
        // Add other infrastructure services here, e.g., repositories,etc. later

        return services;
    }
}
