using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TransferMarketPlatform.Domain.Entities;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Infrastructure.Tests;

public class RepositoryTests
{
    private static TransferMarketDbContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<TransferMarketDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new TransferMarketDbContext(options);
    }

    private static Player CreatePlayer(int id, string name, Country nationality) =>
        new()
        {
            Id = id,
            Name = name,
            Nationality = nationality,
            Age = 25,
            CurrentClub = "Test Club",
            TransferCost = 10000000m,
        };

    [Fact]
    public async Task GetByIdAsync_ReturnsExistingEntity()
    {
        await using var context = CreateContext($"repo_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);
        var player = CreatePlayer(1, "Alice", Country.Brazil);

        await repository.CreateAsync(player);

        var result = await repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(player.Name, result!.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenEntityDoesNotExist()
    {
        await using var context = CreateContext($"repo_missing_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);

        var result = await repository.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetQuery_ReturnsAllStoredEntities()
    {
        await using var context = CreateContext($"repo_query_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);

        await repository.CreateAsync(CreatePlayer(1, "Alice", Country.Argentina));
        await repository.CreateAsync(CreatePlayer(2, "Bob", Country.France));

        var result = await repository.GetQuery().ToListAsync();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.Name == "Alice");
        Assert.Contains(result, p => p.Name == "Bob");
    }

    [Fact]
    public async Task CreateAsync_PersistsEntityAndAssignsIdentity()
    {
        await using var context = CreateContext($"repo_create_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);
        var player = CreatePlayer(0, "Charlie", Country.Spain);

        var created = await repository.CreateAsync(player);

        Assert.True(created.Id > 0);
        Assert.Equal(1, await context.Players.CountAsync());
    }

    [Fact]
    public async Task UpdateAsync_PersistsChangesToExistingEntity()
    {
        await using var context = CreateContext($"repo_update_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);
        var player = await repository.CreateAsync(CreatePlayer(1, "Diana", Country.Germany));

        player.Name = "Diana Updated";
        await repository.UpdateAsync(player);

        var updated = await context.Players.FindAsync(player.Id);

        Assert.NotNull(updated);
        Assert.Equal("Diana Updated", updated!.Name);
    }

    [Fact]
    public async Task DeleteAsync_RemovesExistingEntityAndReturnsTrue()
    {
        await using var context = CreateContext($"repo_delete_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);
        var player = await repository.CreateAsync(CreatePlayer(1, "Eve", Country.Italy));

        var deleted = await repository.DeleteAsync(player.Id);

        Assert.True(deleted);
        Assert.Null(await repository.GetByIdAsync(player.Id));
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenEntityDoesNotExist()
    {
        await using var context = CreateContext($"repo_delete_missing_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);

        var deleted = await repository.DeleteAsync(404);

        Assert.False(deleted);
    }
}
