using TransferMarketPlatform.Domain.Entities;
using TransferMarketPlatform.Domain.Enums;

namespace Domain.Interfaces;

public interface IPlayerRepository : IRepository<Player, int>
{
    Task<IEnumerable<Player>> GetPlayersByCountries(IEnumerable<Country> countries);
}
