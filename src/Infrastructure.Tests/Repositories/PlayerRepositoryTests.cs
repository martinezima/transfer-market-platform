using Microsoft.EntityFrameworkCore;
using TransferMarketPlatform.Domain.Entities;
using TransferMarketPlatform.Domain.Enums;
using TransferMarketPlatform.Infrastructure.Data;

namespace TransferMarketPlatform.Infrastructure.Repositories.Tests;

public class PlayerRepositoryTests
{
    [Fact]
    public async Task GetPlayersByCountries_ReturnsOnlyPlayersFromRequestedCountries()
    {
        var options = new DbContextOptionsBuilder<TransferMarketDbContext>()
            .UseInMemoryDatabase(databaseName: $"PlayerRepositoryTests_{Guid.NewGuid()}")
            .Options;

        await using var context = new TransferMarketDbContext(options);
        var playerId1 = Guid.NewGuid();
        var playerId2 = Guid.NewGuid();
        var playerId3 = Guid.NewGuid();

        context.Players.AddRange(
            new Player
            {
                Id = playerId1,
                Name = "Lionel Messi",
                Nationality = Country.Argentina,
                Age = 37,
                CurrentClub = "Inter Miami",
                TransferCost = 50000000m,
            },
            new Player
            {
                Id = playerId2,
                Name = "Vinicius Junior",
                Nationality = Country.Brazil,
                Age = 24,
                CurrentClub = "Real Madrid",
                TransferCost = 90000000m,
            },
            new Player
            {
                Id = playerId3,
                Name = "Kylian Mbappé",
                Nationality = Country.France,
                Age = 25,
                CurrentClub = "Real Madrid",
                TransferCost = 180000000m,
            }
        );
        await context.SaveChangesAsync();

        var repository = new PlayerRepository(context);

        var result = await repository.GetPlayersByCountries([Country.Argentina, Country.Brazil]);

        Assert.NotNull(result);

        Assert.Equal(2, result.Count());
        // Assert.Equal(3, result.Count());
        Assert.Contains(result, p => p.Id == playerId1 && p.Nationality == Country.Argentina);
        // Assert.Contains(result, p => p.Id == playerId1 && p.Nationality == Country.Mexico);
        Assert.Contains(result, p => p.Id == playerId2 && p.Nationality == Country.Brazil);
        Assert.DoesNotContain(result, p => p.Id == playerId3);
    }
}
