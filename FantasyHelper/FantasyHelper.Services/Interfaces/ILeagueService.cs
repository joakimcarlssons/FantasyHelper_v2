using FantasyHelper.Data.Models;

namespace FantasyHelper.Services.Interfaces
{
    public interface ILeagueService
    {
        Task<IEnumerable<Team>> ApplyLeagueData(IEnumerable<Team> teamsToUpdate, CancellationToken cancellationToken);
    }
}
