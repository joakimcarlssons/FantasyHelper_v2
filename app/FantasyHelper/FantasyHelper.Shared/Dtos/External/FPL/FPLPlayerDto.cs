using System.Numerics;
using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos.External.FPL
{
    public class FPLPlayerDto
    {
        [JsonPropertyName("id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("second_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("web_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("now_cost")]
        public int Price { get; set; }

        [JsonPropertyName("team")]
        public int TeamId { get; set; }

        [JsonPropertyName("form")]
        public string? Form { get; set; }

        [JsonPropertyName("form_rank")]
        public int FormRank { get; set; }

        [JsonPropertyName("value_season")]
        public string? SeasonValue { get; set; }

        [JsonPropertyName("element_type")]
        public int Position { get; set; }

        [JsonPropertyName("chance_of_playing_this_round")]
        public int? ChanceOfPlayingThisRound { get; set; }

        [JsonPropertyName("chance_of_playing_next_round")]
        public int? ChanceOfPlayingNextRound { get; set; }

        [JsonPropertyName("selected_by_percent")]
        public string? SelectedByPercent { get; set; }

        [JsonPropertyName("yellow_cards")]
        public int YellowCards { get; set; }

        [JsonPropertyName("red_cards")]
        public int RedCards { get; set; }

        [JsonPropertyName("goals_scored")]
        public int Goals { get; set; }

        [JsonPropertyName("assists")]
        public int Assists { get; set; }

        [JsonPropertyName("clean_sheets")]
        public int CleanSheets { get; set; }

        [JsonPropertyName("clean_sheets_per_90")]
        public decimal? CleanSheetsPerMatch { get; set; }

        [JsonPropertyName("goals_conceded")]
        public int GoalsConceded { get; set; }

        [JsonPropertyName("minutes")]
        public int MinutesPlayed { get; set; }

        [JsonPropertyName("saves")]
        public int Saves { get; set; }

        [JsonPropertyName("saves_per_90")]
        public decimal? SavesPerMatch { get; set; }

        [JsonPropertyName("bonus")]
        public int Bonus { get; set; }

        [JsonPropertyName("bps")]
        public int Bps { get; set; }

        [JsonPropertyName("total_points")]
        public int TotalPoints { get; set; }

        [JsonPropertyName("points_per_game")]
        public string? PointsPerGame { get; set; }

        [JsonPropertyName("ep_this")]
        public string? ExpectedPointsCurrentGameweek { get; set; }

        [JsonPropertyName("ep_next")]
        public string? ExpectedPointsNextGameweek { get; set; }

        [JsonPropertyName("corners_and_indirect_freekicks_order")]
        public int? CornersOrder { get; set; }

        [JsonPropertyName("direct_freekicks_order")]
        public int? FreekicksOrder { get; set; }

        [JsonPropertyName("penalties_order")]
        public int? PenaltiesOrder { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("news")]
        public string? News { get; set; }

        [JsonPropertyName("news_added")]
        public DateTime? NewsAdded { get; set; }

        [JsonPropertyName("expected_goals")]
        public string? ExpectedGoals { get; set; }

        [JsonPropertyName("expected_goals_per_90")]
        public decimal? ExpectedGoalsPerMatch { get; set; }

        [JsonPropertyName("expected_assists")]
        public string? ExpectedAssists { get; set; }

        [JsonPropertyName("expected_assists_per_90")]
        public decimal? ExpectedAssistsPerMatch { get; set; }

        [JsonPropertyName("expected_goal_involvements")]
        public string? ExpectedGoalsInvolvement { get; set; }

        [JsonPropertyName("expected_goal_involvements_per_90")]
        public decimal? ExpectedGoalsInvolvementPerMatch { get; set; }

        [JsonPropertyName("expected_goals_conceded")]
        public string? ExpectedGoalsConceded { get; set; }

        [JsonPropertyName("expected_goals_conceded_per_90")]
        public decimal? ExpectedGoalsConcededPerMatch { get; set; }

        [JsonPropertyName("transfers_in_event")]
        public int TransfersIn { get; set; }

        [JsonPropertyName("transfers_out_event")]
        public int TransfersOut { get; set; }
    }
}
