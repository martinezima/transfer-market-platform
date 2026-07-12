using TransferMarketPlatform.Application.DTOs;

namespace TransferMarketPlatform.Application.Interfaces;

public interface IPlayerService
{
    Task<IEnumerable<PlayerDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PlayerDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PlayerDto> CreateAsync(
        CreatePlayerDto playerDto,
        CancellationToken cancellationToken = default
    );

    Task<PlayerDto?> UpdateAsync(
        Guid id,
        UpdatePlayerDto playerDto,
        CancellationToken cancellationToken = default
    );

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> BulkDeleteAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default
    );
}
