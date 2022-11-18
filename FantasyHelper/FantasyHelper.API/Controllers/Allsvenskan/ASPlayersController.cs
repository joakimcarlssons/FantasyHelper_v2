using FantasyHelper.Services.Interfaces;
using FantasyHelper.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static FantasyHelper.Services.Setup;

namespace FantasyHelper.API.Controllers.Allsvenskan
{
    [ApiController]
    [Route("api/as/players")]
    public class ASPlayersController : ControllerBase
    {
        private readonly ILogger<ASPlayersController> _logger;
        private readonly IPlayersService _playersService;

        public ASPlayersController(ILogger<ASPlayersController> logger, PlayersFactory playersFactory)
        {
            _logger = logger;
            _playersService = playersFactory(FantasyGames.Allsvenskan);
        }
    }
}
