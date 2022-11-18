namespace FantasyHelper.Services.Allsvenskan
{
    public class ASPlayersService : IPlayersService
    {
        private readonly ILogger<ASPlayersService> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository _db;

        public ASPlayersService(ILogger<ASPlayersService> logger, IMapper mapper, DbFactory db)
        {
            _logger = logger;
            _mapper = mapper;
            _db = db(FantasyGames.Allsvenskan);
        }

        public Task<IEnumerable<PlayerPriceChangeDto>> GetPlayersClosestToPriceFall()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlayerPriceChangeDto>> GetPlayersClosestToPriceRise()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerReadDto> GetPlayersWithBestForm(int numberOfPlayers)
        {
            throw new NotImplementedException();
        }
    }
}
