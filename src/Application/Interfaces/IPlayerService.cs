using TransferMarketPlatform.Application.DTOs;

namespace TransferMarketPlatform.Application.Interfaces;

public interface IPlayerService
{
    Task<IEnumerable<PlayerDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PlayerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<PlayerDto> CreateAsync(
        CreatePlayerDto playerDto,
        CancellationToken cancellationToken = default
    );

    Task<PlayerDto?> UpdateAsync(
        int id,
        UpdatePlayerDto playerDto,
        CancellationToken cancellationToken = default
    );

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> BulkDeleteAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
}
