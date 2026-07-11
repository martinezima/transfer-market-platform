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

        await repository.Create(player);

        var result = await repository.GetById(1);

        Assert.NotNull(result);
        Assert.Equal(player.Name, result!.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenEntityDoesNotExist()
    {
        await using var context = CreateContext($"repo_missing_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);

        var result = await repository.GetById(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetQuery_ReturnsAllStoredEntities()
    {
        await using var context = CreateContext($"repo_query_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);

        await repository.Create(CreatePlayer(1, "Alice", Country.Argentina));
        await repository.Create(CreatePlayer(2, "Bob", Country.France));

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

        var created = await repository.Create(player);

        Assert.True(created.Id > 0);
        Assert.Equal(1, await context.Players.CountAsync());
    }

    [Fact]
    public async Task UpdateAsync_PersistsChangesToExistingEntity()
    {
        await using var context = CreateContext($"repo_update_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);
        var player = await repository.Create(CreatePlayer(1, "Diana", Country.Germany));

        player.Name = "Diana Updated";
        await repository.Update(player);

        var updated = await context.Players.FindAsync(player.Id);

        Assert.NotNull(updated);
        Assert.Equal("Diana Updated", updated!.Name);
    }

    [Fact]
    public async Task DeleteAsync_RemovesExistingEntityAndReturnsTrue()
    {
        await using var context = CreateContext($"repo_delete_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);
        var player = await repository.Create(CreatePlayer(1, "Eve", Country.Italy));

        var deleted = await repository.Delete(player.Id);

        Assert.True(deleted);
        Assert.Null(await repository.GetById(player.Id));
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenEntityDoesNotExist()
    {
        await using var context = CreateContext($"repo_delete_missing_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);

        var deleted = await repository.Delete(404);

        Assert.False(deleted);
    }

    [Fact]
    public async Task BulkDeleteByIds_DeletesSpecifiedEntitiesAndReturnsDeletedCount()
    {
        await using var context = CreateContext($"repo_bulkdelete_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);

        await repository.Create(CreatePlayer(1, "A", Country.Argentina));
        await repository.Create(CreatePlayer(2, "B", Country.France));
        await repository.Create(CreatePlayer(3, "C", Country.Brazil));

        var deletedCount = await repository.BulkDeleteByIds(new[] { 1, 3 });

        Assert.Equal(2, deletedCount);
        Assert.Null(await repository.GetById(1));
        Assert.Null(await repository.GetById(3));
        Assert.NotNull(await repository.GetById(2));
        Assert.Equal(1, await context.Players.CountAsync());
    }

    [Fact]
    public async Task BulkDeleteByIds_ReturnsZero_WhenNoIdsMatch()
    {
        await using var context = CreateContext($"repo_bulkdelete_none_{Guid.NewGuid()}");
        var repository = new Repository<Player, int>(context);

        await repository.Create(CreatePlayer(1, "A", Country.Germany));
        await repository.Create(CreatePlayer(2, "B", Country.Italy));

        var deletedCount = await repository.BulkDeleteByIds(new[] { 999, 1000 });

        Assert.Equal(0, deletedCount);
        Assert.Equal(2, await context.Players.CountAsync());
    }
}
