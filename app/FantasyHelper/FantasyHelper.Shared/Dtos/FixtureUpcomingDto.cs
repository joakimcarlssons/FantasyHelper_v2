using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos
{
    public class FixtureUpcomingDto
    {
        [JsonPropertyName("gameweek")]
        public int GameweekId { get; set; }

        [JsonPropertyName("home_team")]
        public string? HomeTeamShortName { get; set; }

        [JsonPropertyName("home_team_difficulty")]
        public int HomeTeamDifficulty { get; set; }

        [JsonPropertyName("away_team")]
        public string? AwayTeamShortName { get; set; }

        [JsonPropertyName("away_team_difficulty")]
        public int AwayTeamDifficulty { get; set; }
    }
}
