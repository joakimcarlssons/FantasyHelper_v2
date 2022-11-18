using FantasyHelper.Shared.Dtos;

namespace FantasyHelper.Services.Interfaces
{
    public interface IPlayersService
    {
        IEnumerable<PlayerReadDto> GetPlayersWithBestForm(int numberOfPlayers);
        Task<IEnumerable<PlayerPriceChangeDto>> GetPlayersClosestToPriceRise();
        Task<IEnumerable<PlayerPriceChangeDto>> GetPlayersClosestToPriceFall();
    }
}
