using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FPLPricePredictions.Function.Dtos
{
    public class PlayerPriceChangeDto
    {
        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("current_price")]
        public decimal? Price { get; set; }

        [JsonPropertyName("price_target")]
        public string? PriceTarget { get; set; }

        [JsonPropertyName("team_name")]
        public string? TeamName { get; set; }
    }

    public class PriceChangingPlayersDto
    {
        [JsonPropertyName("rising_players")]
        public IEnumerable<PlayerPriceChangeDto> RisingPlayers { get; set; }

        [JsonPropertyName("falling_players")]
        public IEnumerable<PlayerPriceChangeDto> FallingPlayers { get; set; }
    }
}
