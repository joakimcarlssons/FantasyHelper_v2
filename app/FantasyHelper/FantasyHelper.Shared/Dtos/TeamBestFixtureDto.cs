using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos
{
    public class TeamBestFixtureDto
    {
        [JsonPropertyName("team_name")]
        public string? Name { get; set; }

        [JsonPropertyName("short_name")]
        public string? ShortName { get; set; }

        [JsonPropertyName("players")]
        public IEnumerable<PlayerTopPerformerDto>? Players { get; set; }

        [JsonPropertyName("home_fixtures")]
        public IEnumerable<FixtureUpcomingDto>? HomeFixtures { get; set; }

        [JsonPropertyName("away_fixtures")]
        public IEnumerable<FixtureUpcomingDto>? AwayFixtures { get; set; }
    }
}
