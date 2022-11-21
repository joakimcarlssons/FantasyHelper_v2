using FantasyHelper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

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

        [HttpGet("news", Name = "GetPlayerNews")]
        public ActionResult GetPlayerNews(DateTime from)
        {
            try
            {
                _logger.LogInformation("--> Request received to GetPlayerNews...");
                var players = _playersService.GetPlayerNews(from).OrderBy(p => p.NewsAdded);
                return Ok(players);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to handle request for GetPlayerNews: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("transfers", Name = "GetTransferredPlayers")]
        public ActionResult GetTransferredPlayers(int amount = 5)
        {
            if (amount <= 0) return BadRequest();

            try
            {
                _logger.LogInformation("--> Request received to GetTransferredPlayers...");
                var players = _playersService.GetTransferredPlayers(amount);
                return Ok(players);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to handle request for GetTransferredPlayers: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("risking-suspension", Name = "GetPlayersRiskingSuspension")]
        public ActionResult GetPlayersRiskingSuspension()
        {
            try
            {
                _logger.LogInformation("--> Request received to GetPlayersRiskingSuspension...");
                var players = _playersService.GetPlayersRiskingSuspension();
                return Ok(players);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to handle request for GetPlayersRiskingSuspension: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("goalkeepers", Name = "GetTopGoalkeepers")]
        public ActionResult GetTopGoalkeepers(int amount = 5)
        {
            try
            {
                _logger.LogInformation("--> Request received to GetTopGoalkeepers...");

                var players = _playersService.GetBestPlayers(Shared.Enums.PlayerPositions.Goalkeeper, amount);
                return Ok(players);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get top goalkeepers: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("defenders", Name = "GetTopDefenders")]
        public ActionResult GetTopDefenders(int amount = 5)
        {
            try
            {
                _logger.LogInformation("--> Request received to GetTopDefenders...");

                var players = _playersService.GetBestPlayers(Shared.Enums.PlayerPositions.Defender, amount);
                return Ok(players);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get top defenders: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("midfielders", Name = "GetTopMidfielders")]
        public ActionResult GetTopMidfielders(int amount = 5)
        {
            try
            {
                _logger.LogInformation("--> Request received to GetTopMidfielders...");

                var players = _playersService.GetBestPlayers(Shared.Enums.PlayerPositions.Midfielder, amount);
                return Ok(players);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get top midfielders: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("attackers", Name = "GetTopAttackers")]
        public ActionResult GetTopAttackers(int amount = 5)
        {
            try
            {
                _logger.LogInformation("--> Request received to GetTopAttackers...");

                var players = _playersService.GetBestPlayers(Shared.Enums.PlayerPositions.Attacker, amount);
                return Ok(players);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get top attackers: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
