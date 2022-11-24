using FantasyHelper.Data.Models;
using FantasyHelper.Services.Interfaces;
using FantasyHelper.Shared.Dtos;
using FantasyHelper.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace FantasyHelper.Client.Pages.Dashboard
{
    public partial class Dashboard
    {
        [Inject]
        public IPlayersService? PlayersService { get; set; }

        [Inject]
        public ITeamsService? TeamsService { get; set; }

        private string? ChosenGame { get; set; } = "FPL";
        private void SetChosenGame(string game) => ChosenGame = game;

        private PlayerPositions SelectedPlayerPosition { get; set; } = PlayerPositions.Goalkeeper;
        private void SetSelectedPosition(object value)
        {
            switch (value.ToString())
            {
                case "Goalkeeper":
                    SelectedPlayerPosition = PlayerPositions.Goalkeeper;
                    break;
                case "Defender":
                    SelectedPlayerPosition = PlayerPositions.Defender;
                    break;
                case "Midfielder":
                    SelectedPlayerPosition = PlayerPositions.Midfielder;
                    break;
                case "Attacker":
                    SelectedPlayerPosition = PlayerPositions.Attacker;
                    break;
            }
        }

        private IEnumerable<FixtureUpcomingDto> CombineFixtures(TeamBestFixtureDto team)
        {
            if (team is null) return Array.Empty<FixtureUpcomingDto>();

            var fixtures = new List<FixtureUpcomingDto>();
            fixtures.AddRange(team.HomeFixtures ?? Array.Empty<FixtureUpcomingDto>());
            fixtures.AddRange(team.AwayFixtures ?? Array.Empty<FixtureUpcomingDto>());
            return fixtures.OrderBy(f => f.GameweekId);
        }

        private string SetFixtureBackground(FixtureUpcomingDto fixture, bool isHome)
        {
            if (isHome)
            {
                return fixture.HomeTeamDifficulty < 3 ? "background:#01fc7a;"
                    : fixture.HomeTeamDifficulty == 4 ? "background:#ff1751"
                    : fixture.HomeTeamDifficulty == 5 ? "background:#80072d"
                    : "background:lightgray;";
            }
            else
            {
                return fixture.AwayTeamDifficulty < 3 ? "background:#01fc7a;"
                    : fixture.AwayTeamDifficulty == 4 ? "background:#ff1751"
                    : fixture.AwayTeamDifficulty == 5 ? "background:#80072d"
                    : "background:lightgray;";
            }
        }
    }
}
