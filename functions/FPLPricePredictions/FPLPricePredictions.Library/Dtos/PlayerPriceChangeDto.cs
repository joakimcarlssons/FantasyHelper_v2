using System.Text.Json.Serialization;

namespace FPLPricePredictions.Library.Dtos
{
    public class PlayerPriceChangeDto
    {
        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("price_target")]
        public string? PriceTarget { get; set; }

        [JsonPropertyName("team_name")]
        public string? TeamName { get; set; }
    }
}
