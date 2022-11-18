using FantasyHelper.Data.Models;

namespace FantasyHelper.Services.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<Player>> GetPlayers(HttpClient client, CancellationToken cancellationToken = default);
        Task<IEnumerable<Team>> GetTeams(HttpClient client, CancellationToken cancellationToken = default);
        Task<IEnumerable<Gameweek>> GetGameweeks(HttpClient client, CancellationToken cancellationToken = default);
        Task<IEnumerable<Fixture>> GetFixtures(HttpClient client, CancellationToken cancellationToken = default);
    }
}
