using FantasyHelper.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

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
                _logger.LogInformation("--> Fetching price changing players...");

                using var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("x-functions-key", _options.FunctionsKey);

                var response = await client.GetFromJsonAsync<PriceChangingPlayersDto>(_options.PriceFunctionEndpoint);
                if (response is null || response.RisingPlayers is null || response.FallingPlayers is null) throw new NullReferenceException("No players received");


                _logger.LogInformation("--> Price changing players found and parsed as expected!");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("--> Failed to fetch price changing players: {ex.Message}", ex.Message);
                throw;
            }
        }

        public IEnumerable<PlayerNewsDto> GetPlayerNews(DateTime fromDate)
        {
            try
            {
                _logger.LogInformation("--> Getting player news from {fromDate}", fromDate);

                var players = _db.GetPlayers(p => p.NewsAdded != null && p.NewsAdded > fromDate, true);
                return _mapper.Map<IEnumerable<PlayerNewsDto>>(players);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get player news from {fromDate}: {ex.Message}", fromDate, ex.Message);
                throw;
            }
        }

        public IEnumerable<PlayerTopPerformerDto> GetBestPlayers(PlayerPositions position, int amount = 5)
        {
            try
            {
                _logger.LogInformation("--> Getting the {amount} best players of position {position.ToString()}", amount, position.ToString());

                var players = _db.GetPlayers(p => p.Position == (int)position, true)
                    .OrderByDescending(p => p.Form)
                    .Take(amount);

                return _mapper.Map<IEnumerable<PlayerTopPerformerDto>>(players);
            }
            catch (Exception ex)
            {
                _logger.LogError("--> Failed to get best players of position {position.ToString()}: {ex.Message}", position.ToString(), ex.Message);
                throw;
            }
        }

        public TransferredPlayersDto GetTransferredPlayers(int amount = 5)
        {
            try
            {
                _logger.LogInformation("--> Getting {amount} most transferred in/out players...", amount);

                var transferredInPlayers = 
                    _mapper.Map<IEnumerable<PlayerTransferDto>>(_db.GetPlayers(null, true).OrderByDescending(p => p.TransfersIn).Take(amount));
                var transferredOutPlayers =
                    _mapper.Map<IEnumerable<PlayerTransferDto>>(_db.GetPlayers(null, true).OrderByDescending(p => p.TransfersOut).Take(amount));

                return new TransferredPlayersDto(transferredInPlayers, transferredOutPlayers);
            }
            catch (Exception ex)
            {
                _logger.LogError("--> Failed to get transferred players: {ex.Message}", ex.Message);
                throw;
            }
        }

        public IEnumerable<PlayerSuspendedDto> GetPlayersRiskingSuspension()
        {
            try
            {
                // 5 yellow cards before 19 matches played
                var players = _db.GetTeams()
                    .Where(t => t.MatchesPlayed < 19)
                    .Include(t => t.Players)
                    .ThenInclude(p => p.Team)
                    .SelectMany(t => t.Players.Where(p => p.YellowCards == 4))
                    .ToList();

                // 10 yellow cards before 32 matches played
                players.AddRange(_db.GetTeams()
                    .Where(t => t.MatchesPlayed < 32)
                    .Include(t => t.Players.Where(p => p.YellowCards == 9))
                    .Where(t => t.Players.Count > 0)
                    .SelectMany(t => t.Players.Where(p => p.YellowCards == 9)));

                return _mapper.Map<IEnumerable<PlayerSuspendedDto>>(players);
            }
            catch (Exception ex)
            {
                _logger.LogError("--> Failed to get players risking suspension: {ex.Message}", ex.Message);
                throw;
            }
        }
    }
}
