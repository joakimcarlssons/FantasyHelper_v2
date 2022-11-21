using FantasyHelper.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FantasyHelper.Data.FPL
{
    public class FPLRepository : IRepository
    {
        private readonly ILogger<FPLRepository> _logger;
        private readonly FPLDataContext _dbContext;

        public FPLRepository(ILogger<FPLRepository> logger, FPLDataContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default) => (await _dbContext.SaveChangesAsync(cancellationToken)) >= 0;

        public async Task AddOrUpdateFixturesAsync(IEnumerable<Fixture> fixtures, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Fixtures.Any())
            {
                _logger.LogInformation("--> Updating existing fixtures in db");
                _dbContext.Fixtures.UpdateRange(fixtures.Where(f =>
                    _dbContext.Fixtures.Select(fe => fe.FixtureId).Contains(f.FixtureId)));

                _logger.LogInformation("--> Adding new fixtures to db...");
                await _dbContext.Fixtures.AddRangeAsync(fixtures.Where(f =>
                    _dbContext.Fixtures.FirstOrDefault(fe => fe.GameweekId == f.GameweekId) == null), cancellationToken);
            }
            else
            {
                // If no fixtures have been added
                _logger.LogInformation("--> Adding initial load of FPL fixtures to db...");
                await _dbContext.Fixtures.AddRangeAsync(fixtures, cancellationToken);
            }
        }

        public async Task AddOrUpdateGameweeksAsync(IEnumerable<Gameweek> gameweeks, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Gameweeks.Any())
            {
                _logger.LogInformation("--> Updating existing gameweeks in db");
                _dbContext.Gameweeks.UpdateRange(gameweeks.Where(gw =>
                    _dbContext.Gameweeks.Select(gwe => gwe.GameweekId).Contains(gw.GameweekId)));

                _logger.LogInformation("--> Adding new gameweeks to db...");
                await _dbContext.Gameweeks.AddRangeAsync(gameweeks.Where(gw =>
                    _dbContext.Gameweeks.FirstOrDefault(gwe => gwe.GameweekId == gw.GameweekId) == null), cancellationToken);
            }
            else
            {
                // If no gameweeks have been added
                _logger.LogInformation("--> Adding initial load of FPL gameweeks to db...");
                await _dbContext.Gameweeks.AddRangeAsync(gameweeks, cancellationToken);
            }
        }

        public async Task AddOrUpdatePlayersAsync(IEnumerable<Player> players, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Players.Any())
            {
                _logger.LogInformation("--> Updating existing FPL players in db...");
                _dbContext.Players.UpdateRange(players.Where(p =>
                    _dbContext.Players.Select(pe => pe.PlayerId).Contains(p.PlayerId)));

                _logger.LogInformation("--> Adding new FPL players to db...");
                await _dbContext.Players.AddRangeAsync(players.Where(p =>
                    _dbContext.Players.FirstOrDefault(pe => pe.PlayerId == p.PlayerId) == null), cancellationToken);
            }
            else
            {
                // If no players have been added
                _logger.LogInformation("--> Adding initial load of FPL players to db...");
                await _dbContext.Players.AddRangeAsync(players, cancellationToken);
            }
        }

        public async Task AddOrUpdateTeamsAsync(IEnumerable<Team> teams, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Teams.Any())
            {
                _logger.LogInformation("--> Updating existing FPL teams in db...");
                _dbContext.Teams.UpdateRange(teams.Where(t =>
                    _dbContext.Teams.Select(te => te.TeamId).Contains(t.TeamId)));

                _logger.LogInformation("--> Adding new FPL teams to db...");
                await _dbContext.Teams.AddRangeAsync(teams.Where(t =>
                    _dbContext.Teams.FirstOrDefault(te => te.TeamId == t.TeamId) == null), cancellationToken);
            }
            else
            {
                // If no teams have been added
                _logger.LogInformation("--> Adding initial load of FPL teams to db...");
                await _dbContext.Teams.AddRangeAsync(teams, cancellationToken);
            }
        }

        public IEnumerable<Player> GetPlayers(Func<Player, bool>? filter, bool includeTeam)
        {
            _logger.LogInformation("--> Getting FPL players...");
            return includeTeam
                ? _dbContext.Players.Include(p => p.Team).Where(filter ?? (p => true))
                : _dbContext.Players.Where(filter ?? (p => true));
        }

        public IQueryable<Team> GetTeams()
        {
            _logger.LogInformation("--> Getting FPL teams...");
            return _dbContext.Teams.AsQueryable();
        }

        public IEnumerable<Team> GetTeams(Func<Team, bool>? filter)
        {
            _logger.LogInformation("--> Getting FPL teams...");
            return _dbContext.Teams.Where(filter ?? (t => true));
        }

        public IEnumerable<Team> GetTeams(Func<Team, bool>? teamFilter, Func<Player, bool>? playersFilter)
        {
            _logger.LogInformation("--> Getting FPL teams with players...");
            return _dbContext.Teams
                .Include(t => t.Players.Where(playersFilter ?? (p => true)))
                .Where(teamFilter ?? (t => true));
        }

        public IEnumerable<Team> GetTeams(Func<Team, bool>? teamFilter, Func<Fixture, bool>? fixturesFilter)
        {
            _logger.LogInformation("--> Getting FPL teams with fixtures...");
            return _dbContext.Teams
                .Include(t => t.HomeFixtures.Where(fixturesFilter ?? (f => true)))
                .Include(t => t.AwayFixtures.Where(fixturesFilter ?? (f => true)))
                .Where(teamFilter ?? (t => true));
        }

        public IEnumerable<Team> GetTeams(Func<Team, bool>? teamFilter, Func<Player, bool>? playersFilter, Func<Fixture, bool>? fixturesFilter)
        {
            _logger.LogInformation("--> Getting FPL teams with players and fixtures...");
            return _dbContext.Teams
                .Include(t => t.Players.Where(playersFilter ?? (p => true)))
                .Include(t => t.HomeFixtures.Where(fixturesFilter ?? (f => true)))
                .Include(t => t.AwayFixtures.Where(fixturesFilter ?? (f => true)))
                .Where(teamFilter ?? (t => true));
        }

        public IEnumerable<Fixture> GetFixtures(Func<Fixture, bool>? filter, bool includeGameweek)
        {
            _logger.LogInformation("--> Getting FPL fixtures...");
            if (includeGameweek) return _dbContext.Fixtures.Include(f => f.Gameweek).Where(filter ?? (f => true));
            else return _dbContext.Fixtures.Where(filter ?? (f => true));
        }

        public IEnumerable<Gameweek> GetGameweeks(Func<Gameweek, bool>? filter)
        {
            _logger.LogInformation("--> Getting FPL gameweeks...");
            return _dbContext.Gameweeks.Where(filter ?? (gw => true));
        }

        public IEnumerable<Gameweek> GetGameweeks(Func<Gameweek, bool>? gameweekFilter, Func<Fixture, bool>? fixturesFilter)
        {
            _logger.LogInformation("--> Getting FPL gameweeks with fixtures...");
            return _dbContext.Gameweeks.Include(gw => gw.Fixtures.Where(fixturesFilter ?? (f => true))).Where(gameweekFilter ?? (gw => true));
        }
    }
}
