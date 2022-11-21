using System.ComponentModel.DataAnnotations;

namespace FantasyHelper.Data.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DisplayName { get; set; }
        public decimal Price { get; set; }
        public string? Form { get; set; }
        public int FormRank { get; set; }
        public int? ChanceOfPlayingThisRound { get; set; }
        public int? ChanceOfPlayingNextRound { get; set; }
        public int Position { get; set; }
        public int TeamId { get; set; }
        public string? SelectedByPercent { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int CleanSheets { get; set; }
        public decimal? CleanSheetsPerMatch { get; set; }
        public int GoalsConceded { get; set; }
        public int MinutesPlayed { get; set; }
        public int Saves { get; set; }
        public decimal? SavesPerMatch { get; set; }
        public int Bonus { get; set; }
        public int Bps { get; set; }
        public int? CornersOrder { get; set; }
        public int? FreekicksOrder { get; set; }
        public int? PenaltiesOrder { get; set; }
        public string? PriceTarget { get; set; }
        public string? SeasonValue { get; set; }
        public int TotalPoints { get; set; }
        public string? PointsPerGame { get; set; }
        public string? ExpectedPointsCurrentGameweek { get; set; }
        public string? ExpectedPointsNextGameweek { get; set; }
        public string? Status { get; set; }
        public string? News { get; set; }
        public DateTime? NewsAdded { get; set; }
        public string? ExpectedGoals { get; set; }
        public decimal? ExpectedGoalsPerMatch { get; set; }
        public string? ExpectedAssists { get; set; }
        public decimal? ExpectedAssistsPerMatch { get; set; }
        public string? ExpectedGoalsInvolvement { get; set; }
        public decimal? ExpectedGoalsInvolvementPerMatch { get; set; }
        public string? ExpectedGoalsConceded { get; set; }
        public decimal? ExpectedGoalsConcededPerMatch { get; set; }
        public int TransfersIn { get; set; }
        public int TransfersOut { get; set; }

        public Team? Team { get; set; }
    }
}
