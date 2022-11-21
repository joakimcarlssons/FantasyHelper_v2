using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos
{
    public class PlayerTopPerformerDto
    {
        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("team_name")]
        public string? TeamName { get; set; }

        [JsonPropertyName("selected_by")]
        public string? SelectedByPercent { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("form")]
        public string? Form { get; set; }

        [JsonPropertyName("total_points")]
        public int TotalPoints { get; set; }

        [JsonPropertyName("points_per_game")]
        public string? PointsPerGame { get; set; }

        [JsonPropertyName("expected_points_current")]
        public string? ExpectedPointsCurrentGameweek { get; set; }

        [JsonPropertyName("expected_points_next")]
        public string? ExpectedPointsNextGameweek { get; set; }

        [JsonPropertyName("expected_goals")]
        public string? ExpectedGoals { get; set; }

        [JsonPropertyName("expected_goals_per_match")]
        public decimal? ExpectedGoalsPerMatch { get; set; }

        [JsonPropertyName("expected_assists")]
        public string? ExpectedAssists { get; set; }

        [JsonPropertyName("expected_assists_per_match")]
        public decimal? ExpectedAssistsPerMatch { get; set; }

        [JsonPropertyName("expected_goals_involvement")]
        public string? ExpectedGoalsInvolvement { get; set; }

        [JsonPropertyName("expected_goals_involvement_per_match")]
        public decimal? ExpectedGoalsInvolvementPerMatch { get; set; }

        [JsonPropertyName("expected_goals_conceded")]
        public string? ExpectedGoalsConceded { get; set; }

        [JsonPropertyName("expected_goals_conceded_per_match")]
        public decimal? ExpectedGoalsConcededPerMatch { get; set; }
    }
}
