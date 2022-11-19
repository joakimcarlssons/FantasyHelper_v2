using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos.External.Allsvenskan
{
    public class ASFixtureDto
    {
        [JsonPropertyName("id")]
        public int FixtureId { get; set; }

        [JsonPropertyName("event")]
        public int GameweekId { get; set; }

        [JsonPropertyName("team_h")]
        public int HomeTeamId { get; set; }

        [JsonPropertyName("team_h_difficulty")]
        public int HomeTeamDifficulty { get; set; }

        [JsonPropertyName("team_h_score")]
        public int? HomeTeamScore { get; set; }

        [JsonPropertyName("team_a")]
        public int AwayTeamId { get; set; }

        [JsonPropertyName("team_a_difficulty")]
        public int AwayTeamDifficulty { get; set; }

        [JsonPropertyName("team_a_score")]
        public int? AwayTeamScore { get; set; }

        [JsonPropertyName("finished")]
        public bool IsFinished { get; set; }
    }
}
