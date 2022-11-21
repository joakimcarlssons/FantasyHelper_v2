using FantasyHelper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FantasyHelper.API.Controllers.FPL
{
    [ApiController]
    [Route("api/fpl/teams")]
    public class FPLTeamsController : ControllerBase
    {
        private readonly ILogger<FPLTeamsController> _logger;
        private readonly ITeamsService _teamsService;

        public FPLTeamsController(ILogger<FPLTeamsController> logger, ITeamsService teamsService)
        {
            _logger = logger;
            _teamsService = teamsService;
        }

        [HttpGet("best-fixtures", Name = "GetTeamsWithBestFixtures")]
        public ActionResult GetTeamsWithBestFixtures(int amountOfTeams = 5, int amountOfFixtures = 3, int fromGameweek = -1)
        {
            try
            {
                _logger.LogInformation("--> Request received for GetTeamsWithBestFixtures...");

                var teams = _teamsService.GetTeamsWithBestFixtures(amountOfTeams, amountOfFixtures, fromGameweek);
                return Ok(teams);
            }
            catch (Exception ex)
            {
                _logger.LogError("--> Failed to handle request for GetTeamsWithBestFixtures: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
