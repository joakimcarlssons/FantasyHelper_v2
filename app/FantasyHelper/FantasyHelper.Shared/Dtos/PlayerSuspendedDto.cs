using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos
{
    public class PlayerSuspendedDto
    {
        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("team_name")]
        public string? TeamName { get; set; }

        [JsonPropertyName("yellow_cards")]
        public int YellowCards { get; set; }

        [JsonPropertyName("red_cards")]
        public int RedCards { get; set; }
    }
}
