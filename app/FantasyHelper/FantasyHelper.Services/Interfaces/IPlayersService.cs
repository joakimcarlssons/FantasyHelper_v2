using FantasyHelper.Shared.Dtos;

namespace FantasyHelper.Services.Interfaces
{
    public interface IPlayersService
    {
        IEnumerable<PlayerReadDto> GetPlayersWithBestForm(int numberOfPlayers);
        Task<PriceChangingPlayersDto> GetPriceChangingPlayers();
        IEnumerable<PlayerNewsDto> GetPlayerNews(DateTime fromDate);
    }
}
