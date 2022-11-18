using FantasyHelper.Data.Models;

namespace FantasyHelper.Data
{
    public interface IRepository
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task AddOrUpdatePlayersAsync(IEnumerable<Player> players, CancellationToken cancellationToken = default);
        Task AddOrUpdateTeamsAsync(IEnumerable<Team> teams, CancellationToken cancellationToken = default);
        Task AddOrUpdateGameweeksAsync(IEnumerable<Gameweek> gameweeks, CancellationToken cancellationToken = default);
        Task AddOrUpdateFixturesAsync(IEnumerable<Fixture> fixtures, CancellationToken cancellationToken = default);

        IEnumerable<Player> GetPlayers(Func<Player, bool>? filter, bool includeTeam);
        IEnumerable<Team> GetTeams(Func<Team, bool>? filter, CancellationToken cancellationToken = default);
        IEnumerable<Team> GetTeams(Func<Team, bool>? teamFilter, Func<Player, bool>? playersFilter);
        IEnumerable<Team> GetTeams(Func<Team, bool>? teamFilter, Func<Fixture, bool>? fixturesFilter);
        IEnumerable<Team> GetTeams(Func<Team, bool>? teamFilter, Func<Player, bool>? playersFilter, Func<Fixture, bool>? fixturesFilter);
        IEnumerable<Fixture> GetFixtures(Func<Fixture, bool>? filter, bool includeGameweek);
        IEnumerable<Gameweek> GetGameweeks(Func<Gameweek, bool>? filter);
        IEnumerable<Gameweek> GetGameweeks(Func<Gameweek, bool>? gameweekFilter, Func<Fixture, bool>? fixturesFilter);
    }
}
