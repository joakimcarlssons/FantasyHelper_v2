namespace FantasyHelper.Services.Interfaces
{
    public interface ITeamsService
    {
        IEnumerable<TeamBestFixtureDto> GetTeamsWithBestFixtures(int amountOfTeams, int amountOfFixtures, int fromGameweek);
    }
}
