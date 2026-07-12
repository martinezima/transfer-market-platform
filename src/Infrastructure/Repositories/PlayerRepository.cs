using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using TransferMarketPlatform.Domain.Entities;
using TransferMarketPlatform.Domain.Enums;
using TransferMarketPlatform.Infrastructure.Data;

namespace TransferMarketPlatform.Infrastructure.Repositories;

public class PlayerRepository : Repository<Player, Guid>, IPlayerRepository
{
    public PlayerRepository(TransferMarketDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Player>> GetPlayersByCountries(IEnumerable<Country> countries)
    {
        return await GetQuery().Where(p => countries.Contains(p.Nationality)).ToListAsync();
    }
}
