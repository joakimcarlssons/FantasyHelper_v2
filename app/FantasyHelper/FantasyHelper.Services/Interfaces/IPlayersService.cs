using FantasyHelper.Shared.Dtos;

namespace FantasyHelper.Services.Interfaces
{
    public interface IPlayersService
    {
        Task<PriceChangingPlayersDto> GetPriceChangingPlayers();
        IEnumerable<PlayerNewsDto> GetPlayerNews(DateTime fromDate);
        IEnumerable<PlayerTopPerformerDto> GetBestPlayers(PlayerPositions position, int amount = 5);

        TransferredPlayersDto GetTransferredPlayers(int amount = 5);
    }
}
