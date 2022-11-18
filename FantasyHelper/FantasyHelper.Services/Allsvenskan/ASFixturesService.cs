namespace FantasyHelper.Services.Allsvenskan
{
    public class ASFixturesService : IFixturesService
    {
        private readonly ILogger<ASFixturesService> _logger;

        public ASFixturesService(ILogger<ASFixturesService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Updates the fixture difficulty based on team performances, placings, form etc.
        /// </summary>
        /// <remarks>
        /// Min difficulty is 2.
        /// Max difficulty is 5.
        /// 
        /// Difficulty calculation:
        ///     Team playing at home        += 1
        ///     Team playing away           += 2
        ///     Opponent is top 5           += 3
        ///     Team with better placing    += 1
        ///     Team with worse placing     += 2
        ///       -> Placing > 4 places     += 3
        ///       -> Placing > 8 places     -= 1 (for team with better placing)
        /// </remarks>
        public IEnumerable<Fixture> UpdateFixturesDifficulty(IEnumerable<Fixture> fixtures, IEnumerable<Team> teams)
        {
            if (fixtures is null || !fixtures.Any()) throw new Exception("No fixtures provided.");
            else if (teams is null || !teams.Any()) throw new Exception("No teams provided.");

            foreach (var fixture in fixtures)
            {
                fixture.AwayTeamDifficulty = fixture.HomeTeamDifficulty = 0;

                var homeTeam = teams.FirstOrDefault(team => team.TeamId == fixture.HomeTeamId);
                if (homeTeam is null) throw new NullReferenceException("No home team found.");

                var awayTeam = teams.FirstOrDefault(team => team.TeamId == fixture.AwayTeamId);
                if (awayTeam is null) throw new NullReferenceException("No away team found.");

                fixture.AwayTeamDifficulty += 1;
                fixture.HomeTeamDifficulty += 2;

                if (homeTeam.Position <= 5) fixture.HomeTeamDifficulty += 3;
                if (awayTeam.Position <= 5) fixture.AwayTeamDifficulty += 3;

                if (awayTeam.Position > homeTeam.Position)
                {
                    fixture.AwayTeamDifficulty += 1;

                    if ((awayTeam.Position - homeTeam.Position) > 4) fixture.HomeTeamDifficulty += 3;
                    else fixture.HomeTeamDifficulty += 2;

                    if ((awayTeam.Position - homeTeam.Position) > 8) fixture.AwayTeamDifficulty -= 1;
                }
                else
                {
                    fixture.HomeTeamDifficulty += 1;

                    if ((homeTeam.Position - awayTeam.Position) > 4) fixture.AwayTeamDifficulty += 3;
                    else fixture.AwayTeamDifficulty += 2;

                    if ((homeTeam.Position - awayTeam.Position) > 8) fixture.HomeTeamDifficulty -= 1;
                }

                if (fixture.HomeTeamDifficulty < 2) fixture.HomeTeamDifficulty = 2;
                if (fixture.AwayTeamDifficulty < 2) fixture.AwayTeamDifficulty = 2;

                if (fixture.HomeTeamDifficulty > 5) fixture.HomeTeamDifficulty = 5;
                if (fixture.AwayTeamDifficulty > 5) fixture.AwayTeamDifficulty = 5;
            }

            return fixtures;
        }
    }
}
