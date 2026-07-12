using Microsoft.AspNetCore.Mvc;
using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Application.Interfaces;

namespace TransferMarketPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetAll(
            CancellationToken cancellationToken = default
        )
        {
            var players = await _playerService.GetAllAsync(cancellationToken);
            return Ok(players);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<UpdatePlayerDto>> GetById(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            var player = await _playerService.GetByIdAsync(id, cancellationToken);
            return player is null ? NotFound() : Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult<PlayerDto>> Create(
            [FromBody] CreatePlayerDto playerDto,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                var createdPlayer = await _playerService.CreateAsync(playerDto, cancellationToken);
                return CreatedAtAction(
                    nameof(GetAll),
                    new { id = createdPlayer.Id },
                    createdPlayer
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<PlayerDto>> Update(
            Guid id,
            [FromBody] UpdatePlayerDto playerDto,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                var updatedPlayer = await _playerService.UpdateAsync(
                    id,
                    playerDto,
                    cancellationToken
                );
                return Ok(updatedPlayer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<bool>> Delete(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                var deleted = await _playerService.DeleteAsync(id, cancellationToken);
                return deleted ? NoContent() : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
