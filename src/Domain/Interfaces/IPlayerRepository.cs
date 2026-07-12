using TransferMarketPlatform.Domain.Entities;
using TransferMarketPlatform.Domain.Enums;

namespace Domain.Interfaces;

public interface IPlayerRepository : IRepository<Player, Guid>
{
    Task<IEnumerable<Player>> GetPlayersByCountries(IEnumerable<Country> countries);
}
