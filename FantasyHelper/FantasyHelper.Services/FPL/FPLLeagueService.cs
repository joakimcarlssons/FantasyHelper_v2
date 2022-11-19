using FantasyHelper.Data.Models;
using FantasyHelper.Services.Helpers;
using FantasyHelper.Services.Interfaces;
using FantasyHelper.Shared.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FantasyHelper.Services.FPL
{
    public class FPLLeagueService : ILeagueService
    {
        private readonly ILogger<FPLLeagueService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly FPLOptions _options;

        public FPLLeagueService(ILogger<FPLLeagueService> logger, IHttpClientFactory httpClientFactory, IOptions<FPLOptions> options)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        public async Task<IEnumerable<Team>> ApplyLeagueData(IEnumerable<Team> teamsToUpdate, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("--> Starting load of league table data for Premier League...");

                using var client = _httpClientFactory.CreateClient();
                await teamsToUpdate.AddLeagueTableData(client, _options.LeagueEndpoint, cancellationToken);

                _logger.LogInformation("--> Load of league table data for Premier League completed!");
                return teamsToUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogError("--> Failed to load league table data for Allsvenskan: {ex.Message}", ex.Message);
                throw;
            }
        }
    }
}
