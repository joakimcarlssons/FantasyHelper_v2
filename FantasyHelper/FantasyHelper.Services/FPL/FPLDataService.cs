namespace FantasyHelper.Services.FPL
{
    /// <summary>
    /// Handles the continuous data loading of FPL Data
    /// </summary>
    public class FPLDataService : BackgroundService, IDataService
    {
        private readonly PeriodicTimer _timer;
        private readonly ILogger<FPLDataService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly FPLOptions _options;
        private readonly ILeagueService _leagueService;
        private IRepository? _db;

        public FPLDataService(ILogger<FPLDataService> logger, IHttpClientFactory httpClientFactory, IServiceScopeFactory scopeFactory, IMapper mapper, IOptions<FPLOptions> options, LeagueFactory leagueFactory)
        {
            _options = options.Value;
            if (_options.LoadingInterval <= 0) throw new NullReferenceException("Loading interval is not set correctly in appsettings.");

            _timer = new(TimeSpan.FromMinutes(_options.LoadingInterval));

            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _leagueService = leagueFactory(FantasyGames.FPL);
        }

        #region Loading Methods

        public async Task<IEnumerable<Fixture>> GetFixtures(HttpClient client, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading FPL fixtures...");
                var fixtures = await client.GetFromJsonAsync<IEnumerable<FPLFixtureDto>>(_options.FixturesEndpoint, cancellationToken);

                var result = _mapper.Map<IEnumerable<Fixture>>(fixtures);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load FPL fixtures: {ex.Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Gameweek>> GetGameweeks(HttpClient client, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading FPL gameweeks...");
                var rootData = await client.GetFromJsonAsync<FPLRootDto>(_options.RootEndpoint, cancellationToken);

                var result = _mapper.Map<IEnumerable<Gameweek>>(rootData!.Gameweeks);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load FPL gameweeks: {ex.Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Player>> GetPlayers(HttpClient client, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Loading FPL players...");
                var rootData = await client.GetFromJsonAsync<FPLRootDto>(_options.RootEndpoint, cancellationToken);

                var result = _mapper.Map<IEnumerable<Player>>(rootData!.Players);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load FPL players: {ex.Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Team>> GetTeams(HttpClient client, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading FPL teams...");
                var rootData = await client.GetFromJsonAsync<FPLRootDto>(_options.RootEndpoint, cancellationToken);

                var result = _mapper.Map<IEnumerable<Team>>(rootData!.Teams);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load FPL teams: {ex.Message}", ex.Message);
                throw;
            }
        } 

        #endregion

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                if (!_options.ShouldLoad) break;

                try
                {
                    _logger.LogInformation("--> Starting load of FPL data");

                    using var client = _httpClientFactory.CreateClient("FPL");
                    using var scope = _scopeFactory.CreateScope();
                    _db = scope.ServiceProvider.GetRequiredService<DbFactory>().Invoke(FantasyGames.FPL);

                    var players = await GetPlayers(client, stoppingToken);

                    var teams = await GetTeams(client, stoppingToken);
                    teams = await _leagueService.ApplyLeagueData(teams, stoppingToken);

                    var gameweeks = await GetGameweeks(client, stoppingToken);

                    var fixtures = await GetFixtures(client, stoppingToken);

                    await _db.AddOrUpdatePlayersAsync(players, stoppingToken);
                    await _db.AddOrUpdateTeamsAsync(teams, stoppingToken);
                    await _db.AddOrUpdateGameweeksAsync(gameweeks, stoppingToken);
                    await _db.AddOrUpdateFixturesAsync(fixtures, stoppingToken);
                    await _db.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("--> FPL data has been loaded.");
                }
                catch (Exception ex)
                {
                    _logger.LogError("--> Error occured when loading FPL data: {ex.Message}", ex.Message);
                    throw;
                }
            }
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested);
        }
    }
}
