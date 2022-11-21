using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos
{
    public class PlayerNewsDto
    {
        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("team_name")]
        public string? TeamName { get; set; }

        [JsonPropertyName("selected_by")]
        public string? SelectedByPercent { get; set; }

        [JsonPropertyName("news")]
        public string? News { get; set; }

        [JsonPropertyName("news_added")]
        public DateTime? NewsAdded { get; set; }
    }
}
