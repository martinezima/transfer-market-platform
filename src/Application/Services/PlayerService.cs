using System.Linq;
using AutoMapper;
using Domain.Interfaces;
using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Application.Interfaces;
using TransferMarketPlatform.Application.Validators;
using TransferMarketPlatform.Domain.Entities;

namespace TransferMarketPlatform.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _repository;
    private readonly IMapper _mapper;
    private readonly CreatePlayerValidator _createPlayerValidator;
    private readonly UpdatePlayerValidator _updatePlayerValidator;
    private readonly BulkDeletePlayerValidator _deletePlayerValidator;

    public PlayerService(
        IPlayerRepository repository,
        IMapper mapper,
        CreatePlayerValidator createPlayerValidator,
        UpdatePlayerValidator updatePlayerValidator,
        BulkDeletePlayerValidator deletePlayerValidator
    )
    {
        _repository = repository;
        _mapper = mapper;
        _createPlayerValidator = createPlayerValidator;
        _updatePlayerValidator = updatePlayerValidator;
        _deletePlayerValidator = deletePlayerValidator;
    }

    public Task<IEnumerable<PlayerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var players = _repository.GetQuery().Select(p => _mapper.Map<Player, PlayerDto>(p));
        return Task.FromResult<IEnumerable<PlayerDto>>(players.ToList().AsEnumerable());
    }

    public async Task<PlayerDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        var player = await _repository.GetById(id, cancellationToken);
        return player is null ? null : _mapper.Map<Player, PlayerDto>(player);
    }

    public async Task<PlayerDto> CreateAsync(
        CreatePlayerDto playerDto,
        CancellationToken cancellationToken = default
    )
    {
        // Validate the playerDto using the CreatePlayerValidator
        var validationResult = await _createPlayerValidator.ValidateAsync(
            playerDto,
            cancellationToken
        );
        if (!validationResult.IsValid)
        {
            throw new ArgumentException(
                "Invalid player data: "
                    + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }

        var player = _mapper.Map<CreatePlayerDto, Player>(playerDto);

        var createdPlayer = await _repository.Create(player, cancellationToken);
        return _mapper.Map<Player, PlayerDto>(createdPlayer);
    }

    public async Task<PlayerDto?> UpdateAsync(
        int id,
        UpdatePlayerDto playerDto,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await _updatePlayerValidator.ValidateAsync(
            playerDto,
            cancellationToken
        );
        if (!validationResult.IsValid)
        {
            throw new ArgumentException(
                "Invalid player data: "
                    + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }

        var existingPlayer =
            await _repository.GetById(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Player with ID {playerDto.Id} not found");
        existingPlayer.Name = playerDto.Name ?? existingPlayer.Name;
        existingPlayer.Nationality = playerDto.Nationality;
        existingPlayer.Age = playerDto.Age;
        existingPlayer.CurrentClub = playerDto.CurrentClub ?? existingPlayer.CurrentClub;
        existingPlayer.TransferCost = playerDto.TransferCost;

        var updatedPlayer = await _repository.Update(existingPlayer, cancellationToken);
        return _mapper.Map<Player, PlayerDto>(updatedPlayer);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        // Validate the ID using the BulkDeletePlayerValidator
        var validationResult = await _deletePlayerValidator.ValidateAsync(
            new BulkDeletePlayerDto { PlayerIds = new List<int> { id } },
            cancellationToken
        );
        if (!validationResult.IsValid)
        {
            throw new ArgumentException(
                "Invalid player ID: "
                    + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }
        return await _repository.Delete(id, cancellationToken);
    }

    public async Task<bool> BulkDeleteAsync(
        IEnumerable<int> ids,
        CancellationToken cancellationToken = default
    )
    {
        // Validate the IDs using the BulkDeletePlayerValidator
        var validationResult = await _deletePlayerValidator.ValidateAsync(
            new BulkDeletePlayerDto { PlayerIds = ids.ToList() },
            cancellationToken
        );
        if (!validationResult.IsValid)
        {
            throw new ArgumentException(
                "Invalid player IDs: "
                    + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
            );
        }
        var deletedCount = await _repository.BulkDeleteByIds(ids, cancellationToken);
        return deletedCount > 0;
    }
}
