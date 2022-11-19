using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos.External.FPL
{
    public class FPLTeamDto
    {
        [JsonPropertyName("id")]
        public int TeamId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("short_name")]
        public string? ShortName { get; set; }

        [JsonPropertyName("code")]
        public int TeamCode { get; set; }
    }
}