using Microsoft.EntityFrameworkCore;
using TransferMarketPlatform.Domain.Entities;
using TransferMarketPlatform.Infrastructure.Data;

namespace TransferMarketPlatform.Infrastructure.Tests;

public class TransferMarketDbContextTests
{
    [Fact]
    public void Can_create_db_context_with_players_dbset()
    {
        var options = new DbContextOptionsBuilder<TransferMarketDbContext>()
            .UseInMemoryDatabase(databaseName: "TransferMarketDbContextTests")
            .Options;

        using var context = new TransferMarketDbContext(options);
        // Assert.Null(context.Players);
        Assert.NotNull(context.Players);
        Assert.IsAssignableFrom<DbSet<Player>>(context.Players);
    }

    [Fact]
    public void Should_map_player_entity_to_players_table()
    {
        var options = new DbContextOptionsBuilder<TransferMarketDbContext>()
            .UseInMemoryDatabase(databaseName: "TransferMarketDbContextTests_Table")
            .Options;

        using var context = new TransferMarketDbContext(options);

        var entityType = context.Model.FindEntityType(typeof(Player));

        Assert.NotNull(entityType);
        //Assert.Equal("player", entityType!.GetTableName());
        Assert.Equal("PLAYERS", entityType!.GetTableName());
    }

    [Fact]
    public void Should_configure_player_id_as_primary_key_when_model_is_built()
    {
        var options = new DbContextOptionsBuilder<TransferMarketDbContext>()
            .UseInMemoryDatabase(databaseName: "TransferMarketDbContextTests_Key_Built")
            .Options;

        using var context = new TransferMarketDbContext(options);

        var entityType = context.Model.FindEntityType(typeof(Player));
        var primaryKey = entityType?.FindPrimaryKey();

        Assert.NotNull(entityType);
        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey!.Properties);
        Assert.Equal(nameof(Player.Id), primaryKey.Properties[0].Name);
    }
}
