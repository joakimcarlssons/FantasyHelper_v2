using AutoMapper;
using FantasyHelper.Data;
using FantasyHelper.Services.Interfaces;
using FantasyHelper.Shared.Config;
using FantasyHelper.Shared.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FantasyHelper.Services.FPL
{
    public class FPLPlayersService : IPlayersService
    {
        private readonly ILogger<FPLPlayersService> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository _db;
        private FPLOptions _options;

        public FPLPlayersService(ILogger<FPLPlayersService> logger, IMapper mapper, IRepository db, IOptions<FPLOptions> options)
        {
            _logger = logger;
            _mapper = mapper;
            _db = db;
            _options = options.Value;
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
    }
}
