using FantasyHelper.Data.Models;

namespace FantasyHelper.Services.Interfaces
{
    public interface IFixturesService
    {
        IEnumerable<Fixture> UpdateFixturesDifficulty(IEnumerable<Fixture> fixtures, IEnumerable<Team> teams);
    }
}
