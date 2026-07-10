using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransferMarketPlatform.Domain.Entities;

namespace TransferMarketPlatform.Infrastructure.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void AddInfrastructure_RegistersDbContextAndRepositories_ForSqlServer()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["ConnectionStrings:DefaultConnection"] =
                        "Server=(localdb)\\MSSQLLocalDB;Database=TransferMarket;Trusted_Connection=True;",
                }
            )
            .Build();

        var services = new ServiceCollection();

        services.AddInfrastructure(configuration, useSqliteDemo: false);

        using var provider = services.BuildServiceProvider();

        var dbContext = provider.GetService<TransferMarketDbContext>();
        var playerRepository = provider.GetService<IPlayerRepository>();
        var genericRepository = provider.GetService<IRepository<Player, int>>();

        Assert.NotNull(dbContext);
        Assert.NotNull(playerRepository);
        Assert.NotNull(genericRepository);
        Assert.IsType<PlayerRepository>(playerRepository);
        Assert.IsType<Repository<Player, int>>(genericRepository);
    }

    [Fact]
    public void AddInfrastructure_RegistersDbContextAndRepositories_ForSqliteDemo()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["ConnectionStrings:SqliteConnection"] = "Data Source=transfermarket.db",
                }
            )
            .Build();

        var services = new ServiceCollection();

        services.AddInfrastructure(configuration, useSqliteDemo: true);

        using var provider = services.BuildServiceProvider();

        var dbContext = provider.GetService<TransferMarketDbContext>();
        var playerRepository = provider.GetService<IPlayerRepository>();

        Assert.NotNull(dbContext);
        Assert.NotNull(playerRepository);
        Assert.IsType<PlayerRepository>(playerRepository);
    }
}
