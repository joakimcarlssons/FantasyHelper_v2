namespace FantasyHelper.Services.Allsvenskan
{
    public class ASDataService : BackgroundService, IDataService
    {
        private readonly PeriodicTimer _timer;
        private readonly ILogger<ASDataService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly AllsvenskanOptions _options;
        private readonly ILeagueService _leagueService;
        private readonly IFixturesService _fixturesService;

        private IRepository? _db;

        public ASDataService(ILogger<ASDataService> logger, IHttpClientFactory httpClientFactory, 
            IServiceScopeFactory scopeFactory, IOptions<AllsvenskanOptions> options, IMapper mapper,
            LeagueFactory leagueFactory, IFixturesService fixturesService
        ){
            _options = options.Value;
            if (_options.LoadingInterval <= 0) throw new NullReferenceException("Loading interval is not set correctly in appsettings.");

            _timer = new(TimeSpan.FromMinutes(_options.LoadingInterval));

            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _leagueService = leagueFactory(FantasyGames.Allsvenskan);
            _fixturesService = fixturesService;
        }

        #region Loading Methods

        public async Task<IEnumerable<Fixture>> GetFixtures(HttpClient client, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading Allsvenskan fixtures...");
                var fixtures = await client.GetFromJsonAsync<IEnumerable<ASFixtureDto>>(_options.FixturesEndpoint, cancellationToken);

                var result = _mapper.Map<IEnumerable<Fixture>>(fixtures);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load Allsvenskan fixtures: {ex.Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Gameweek>> GetGameweeks(HttpClient client, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading Allsvenskan gameweeks...");
                var rootData = await client.GetFromJsonAsync<ASRootDto>(_options.RootEndpoint, cancellationToken);

                var result = _mapper.Map<IEnumerable<Gameweek>>(rootData!.Gameweeks);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load Allsvenskan gameweeks: {ex.Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Player>> GetPlayers(HttpClient client, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading Allsvenskan players...");
                var rootData = await client.GetFromJsonAsync<ASRootDto>(_options.RootEndpoint, cancellationToken);

                var result = _mapper.Map<IEnumerable<Player>>(rootData!.Players);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load Allsvenskan players: {ex.Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Team>> GetTeams(HttpClient client, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Loading Allsvenskan teams...");
                var rootData = await client.GetFromJsonAsync<ASRootDto>(_options.RootEndpoint, cancellationToken);

                var result = _mapper.Map<IEnumerable<Team>>(rootData!.Teams);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load Allsvenskan teams: {ex.Message}", ex.Message);
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
                    _logger.LogInformation("--> Starting load of Allsvenskan data");

                    using var client = _httpClientFactory.CreateClient("Allsvenskan");
                    using var scope = _scopeFactory.CreateScope();
                    _db = scope.ServiceProvider.GetRequiredService<DbFactory>().Invoke(FantasyGames.Allsvenskan);

                    var players = await GetPlayers(client, stoppingToken);

                    var teams = await GetTeams(client, stoppingToken);
                    teams = await _leagueService.ApplyLeagueData(teams, stoppingToken);

                    var gameweeks = await GetGameweeks(client, stoppingToken);
                    
                    var fixtures = await GetFixtures(client, stoppingToken);
                    fixtures = _fixturesService.UpdateFixturesDifficulty(fixtures, teams);

                    await _db.AddOrUpdatePlayersAsync(players, stoppingToken);
                    await _db.AddOrUpdateTeamsAsync(teams, stoppingToken);
                    await _db.AddOrUpdateGameweeksAsync(gameweeks, stoppingToken);
                    await _db.AddOrUpdateFixturesAsync(fixtures, stoppingToken);
                    await _db.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("--> Allsvenskan data has been loaded.");
                }
                catch (Exception ex)
                {
                    _logger.LogError("--> Error occured when loading Allsvenskan data: {ex.Message}", ex.Message);
                    throw;
                }
            }
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested);
        }
    }
}
