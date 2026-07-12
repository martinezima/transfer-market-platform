using AutoMapper;
using Domain.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Application.Mappings;
using TransferMarketPlatform.Application.Services;
using TransferMarketPlatform.Application.Validators;
using TransferMarketPlatform.Domain.Entities;
using TransferMarketPlatform.Domain.Enums;
using Xunit;

namespace TransferMarketPlatform.Application.Tests.Services;

public class PlayerServiceTests
{
    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile<PlayerProfile>();
            },
            NullLoggerFactory.Instance
        );
        configuration.AssertConfigurationIsValid();
        return configuration.CreateMapper();
    }

    private PlayerService CreateService(IPlayerRepository repo, IMapper mapper)
    {
        return new PlayerService(
            repo,
            mapper,
            new CreatePlayerValidator(),
            new UpdatePlayerValidator(),
            new BulkDeletePlayerValidator()
        );
    }

    private class InMemoryPlayerRepository : IPlayerRepository
    {
        private readonly List<Player> _players = [];
        private int _nextId = 1;

        public InMemoryPlayerRepository(IEnumerable<Player>? seed = null)
        {
            if (seed != null)
            {
                foreach (var p in seed)
                {
                    _players.Add(p);
                    _nextId = Math.Max(_nextId, p.Id + 1);
                }
            }
        }

        public Task<Player?> GetById(int id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_players.FirstOrDefault(p => p.Id == id));
        }

        public IQueryable<Player> GetQuery()
        {
            return _players.AsQueryable();
        }

        public Task<Player> Create(Player entity, CancellationToken cancellationToken = default)
        {
            entity.Id = _nextId++;
            _players.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<Player> Update(Player entity, CancellationToken cancellationToken = default)
        {
            var index = _players.FindIndex(p => p.Id == entity.Id);
            if (index >= 0)
            {
                _players[index] = entity;
                return Task.FromResult(entity);
            }
            throw new KeyNotFoundException();
        }

        public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var removed = _players.RemoveAll(p => p.Id == id) > 0;
            return Task.FromResult(removed);
        }

        public Task<int> BulkDeleteByIds(
            IEnumerable<int> ids,
            CancellationToken cancellationToken = default,
            string keyName = "Id"
        )
        {
            var idSet = ids.ToHashSet();
            var before = _players.Count;
            _players.RemoveAll(p => idSet.Contains(p.Id));
            return Task.FromResult(before - _players.Count);
        }

        public Task<IEnumerable<Player>> GetPlayersByCountries(IEnumerable<Country> countries)
        {
            var set = countries.ToHashSet();
            return Task.FromResult<IEnumerable<Player>>(
                _players.Where(p => set.Contains(p.Nationality)).ToList()
            );
        }
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllPlayers()
    {
        var seed = new[]
        {
            new Player(1, "A", Country.Portugal, 30, "ClubA", 100m),
            new Player(2, "B", Country.France, 22, "ClubB", 200m),
        };
        var repo = new InMemoryPlayerRepository(seed);
        var mapper = CreateMapper();
        var svc = CreateService(repo, mapper);

        var result = await svc.GetAllAsync();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, d => d.Name == "A");
        Assert.Contains(result, d => d.Name == "B");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsPlayer_WhenExists()
    {
        var seed = new[] { new Player(1, "A", Country.Portugal, 30, "ClubA", 100m) };
        var repo = new InMemoryPlayerRepository(seed);
        var mapper = CreateMapper();
        var svc = CreateService(repo, mapper);

        var result = await svc.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("A", result!.Name);
    }

    [Fact]
    public async Task CreateAsync_AddsAndReturnsCreatedPlayer()
    {
        var repo = new InMemoryPlayerRepository();
        var mapper = CreateMapper();
        var svc = CreateService(repo, mapper);

        var createDto = new CreatePlayerDto
        {
            Name = "Mark Cucurrella",
            Nationality = Country.Spain,
            Age = 28,
            CurrentClub = "ClubC",
            TransferCost = 1500000m,
        };

        var created = await svc.CreateAsync(createDto);

        Assert.NotNull(created);
        Assert.Equal("Mark Cucurrella", created.Name);
        Assert.Equal("Spain", created.NationalityDisplayName);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingPlayer()
    {
        var seed = new[] { new Player(1, "Old", Country.Portugal, 30, "OldClub", 100m) };
        var repo = new InMemoryPlayerRepository(seed);
        var mapper = CreateMapper();
        var svc = CreateService(repo, mapper);

        var updateDto = new UpdatePlayerDto
        {
            Id = 1,
            Name = "New",
            Nationality = Country.Brazil,
            Age = 31,
            CurrentClub = "NewClub",
            TransferCost = 900000m,
        };

        var updated = await svc.UpdateAsync(1, updateDto);

        Assert.NotNull(updated);
        Assert.Equal("New", updated!.Name);
        Assert.Equal("Brazil", updated.NationalityDisplayName);
    }

    [Fact]
    public async Task DeleteAsync_DeletesPlayer()
    {
        var seed = new[] { new Player(1, "ToDelete", Country.Portugal, 29, "Club", 50m) };
        var repo = new InMemoryPlayerRepository(seed);
        var mapper = CreateMapper();
        var svc = CreateService(repo, mapper);

        var ok = await svc.DeleteAsync(1);

        Assert.True(ok);
    }

    [Fact]
    public async Task BulkDeleteAsync_DeletesPlayers()
    {
        var seed = new[]
        {
            new Player(1, "A", Country.Portugal, 30, "ClubA", 100m),
            new Player(2, "B", Country.France, 22, "ClubB", 200m),
        };
        var repo = new InMemoryPlayerRepository(seed);
        var mapper = CreateMapper();
        var svc = CreateService(repo, mapper);

        var ok = await svc.BulkDeleteAsync(new[] { 1, 2 });

        Assert.True(ok);
    }
}
