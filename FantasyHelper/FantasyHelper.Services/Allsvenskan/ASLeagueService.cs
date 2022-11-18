namespace FantasyHelper.Services.Allsvenskan
{
    public class ASLeagueService : ILeagueService
    {
        private readonly ILogger<ASLeagueService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AllsvenskanOptions _options;

        public ASLeagueService(ILogger<ASLeagueService> logger, IHttpClientFactory httpClientFactory, IOptions<AllsvenskanOptions> options)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        public async Task<IEnumerable<Team>> ApplyLeagueData(IEnumerable<Team> teamsToUpdate, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("--> Starting load of league table data for Allsvenskan...");

                using var client = _httpClientFactory.CreateClient();
                await teamsToUpdate.AddLeagueTableData(client, _options.LeagueEndpoint, cancellationToken);

                _logger.LogInformation("--> Load of league table data for Allsvenskan completed!");
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
