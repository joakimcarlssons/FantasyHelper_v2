using FantasyHelper.Shared.Dtos;

namespace FantasyHelper.Services.FPL
{
    public class FPLPlayersService : IPlayersService
    {
        private readonly ILogger<FPLPlayersService> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository _db;
        private readonly IHttpClientFactory _httpClientFactory;
        private FPLOptions _options;

        public FPLPlayersService(ILogger<FPLPlayersService> logger, IMapper mapper, IRepository db, IOptions<FPLOptions> options, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _mapper = mapper;
            _db = db;
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PriceChangingPlayersDto> GetPriceChangingPlayers()
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("x-functions-key", _options.FunctionsKey);

                var response = await client.GetFromJsonAsync<PriceChangingPlayersDto>(_options.PriceFunctionEndpoint);
                if (response is null || response.RisingPlayers is null || response.FallingPlayers is null) throw new NullReferenceException("No players received");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to fetch price changing players: {ex.Message}", ex.Message);
                throw;
            }
        }

        public IEnumerable<PlayerReadDto> GetPlayersWithBestForm(int numberOfPlayers)
        {
            try
            {
                return _mapper.Map<IEnumerable<PlayerReadDto>>(
                    _db.GetPlayers(null, false)
                    .OrderByDescending(p => p.Form)
                    .Take(numberOfPlayers));
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<PlayerNewsDto> GetPlayerNews(DateTime fromDate)
        {
            try
            {
                var players = _db.GetPlayers(p => p.NewsAdded != null && p.NewsAdded > fromDate, true);
                return _mapper.Map<IEnumerable<PlayerNewsDto>>(players);
            }
            catch
            {
                throw;
            }
        }
    }
}
