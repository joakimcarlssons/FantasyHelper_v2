using FantasyHelper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FantasyHelper.API.Controllers.FPL
{
    [ApiController]
    [Route("api/fpl/players")]
    public class FPLPlayersController : ControllerBase
    {
        private readonly ILogger<FPLPlayersController> _logger;
        private readonly IPlayersService _playersService;

        public FPLPlayersController(ILogger<FPLPlayersController> logger, IPlayersService playersService)
        {
            _logger = logger;
            _playersService = playersService;
        }

        [HttpGet("form")]
        public ActionResult GetPlayersWithBestForm()
        {
            _logger.LogInformation("--> Request received for FPL players with best form...");

            var players = _playersService.GetPlayersWithBestForm(10);
            return Ok(players);
        }
    }
}
