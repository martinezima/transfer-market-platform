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
    }
}
