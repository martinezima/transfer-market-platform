using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TransferMarketPlatform.Domain.Entities;
using TransferMarketPlatform.Domain.Enums;

public class PlayerRepository : Repository<Player, Guid>, IPlayerRepository
{
    public PlayerRepository(TransferMarketDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Player>> GetPlayersByCountries(IEnumerable<Country> countries)
    {
        return await GetQuery().Where(p => countries.Contains(p.Nationality)).ToListAsync();
    }
}
