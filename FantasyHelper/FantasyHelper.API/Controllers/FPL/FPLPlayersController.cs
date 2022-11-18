using FantasyHelper.Services.Interfaces;
using FantasyHelper.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static FantasyHelper.Services.Setup;

namespace FantasyHelper.API.Controllers.FPL
{
    [ApiController]
    [Route("api/fpl/players")]
    public class FPLPlayersController : ControllerBase
    {
        private readonly ILogger<FPLPlayersController> _logger;
        private readonly IPlayersService _playersService;

        public FPLPlayersController(ILogger<FPLPlayersController> logger, PlayersFactory playersFactory)
        {
            _logger = logger;
            _playersService = playersFactory(FantasyGames.FPL);
        }

        [HttpGet("form")]
        public ActionResult GetPlayersWithBestForm()
        {
            var players = _playersService.GetPlayersWithBestForm(10);
            return Ok(players);
        }

        [HttpGet("pricerise")]
        public async Task<ActionResult> GetRisingPlayers()
        {
            var players = await _playersService.GetPlayersClosestToPriceRise();
            return Ok(players);
        }
    }
}
