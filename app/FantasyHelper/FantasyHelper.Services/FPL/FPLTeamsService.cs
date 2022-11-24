using Microsoft.EntityFrameworkCore;

namespace FantasyHelper.Services.FPL
{
    public class FPLTeamsService : ITeamsService
    {
        private readonly ILogger<FPLTeamsService> _logger;
        private readonly IRepository _db;
        private readonly IMapper _mapper;

        public FPLTeamsService(ILogger<FPLTeamsService> logger, IRepository db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<TeamBestFixtureDto> GetTeamsWithBestFixtures(int amountOfTeams = 5, int amountOfFixtures = 3, int fromGameweek = -1)
        {
            try
            {
                _logger.LogInformation("--> Getting teams with best fixtures...");

                if (fromGameweek == -1)
                {
                    // Get next gameweek
                    fromGameweek = _db.GetGameweeks(gw => gw.IsNext).FirstOrDefault()?.GameweekId ?? 1;
                }

                var teams = _db.GetTeams()
                    .Include(t => t.HomeFixtures.Where(f => f.GameweekId >= fromGameweek && f.GameweekId < (fromGameweek + amountOfFixtures)))
                    .Include(t => t.AwayFixtures.Where(f => f.GameweekId >= fromGameweek && f.GameweekId < (fromGameweek + amountOfFixtures)));
                if (amountOfTeams > teams.Count()) amountOfTeams = teams.Count();

                var selectedTeams = new List<(Team Team, int TotalDifficulty)>();
                foreach (var team in teams)
                {
                    var totalDifficulty = 0;

                    totalDifficulty += team.HomeFixtures.Sum(f => f.HomeTeamDifficulty);
                    totalDifficulty += team.AwayFixtures.Sum(f => f.AwayTeamDifficulty);

                    if (selectedTeams.Count < amountOfTeams)
                    {
                        selectedTeams.Add((team, totalDifficulty));
                    }
                    else
                    {
                        selectedTeams = selectedTeams.OrderByDescending(t => t.TotalDifficulty).ToList();
                        if (selectedTeams[0].TotalDifficulty > totalDifficulty)
                        {
                            selectedTeams[0] = (team, totalDifficulty);
                        }
                    }
                }

                var result = new HashSet<TeamBestFixtureDto>();
                foreach (var team in selectedTeams)
                {
                    result.Add(_mapper.Map<TeamBestFixtureDto>(team.Team));
                }

                return result.OrderBy(t => t.AwayFixtures?.Sum(f => f.AwayTeamDifficulty) + t.HomeFixtures?.Sum(f => f.HomeTeamDifficulty));
            }
            catch (Exception ex)
            {
                _logger.LogError("--> Failed to get teams with best fixtures: {ex.Message}", ex.Message);
                throw;
            }
        }
    }
}
