using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos
{
    public class PlayerReadDto
    {
        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }
    }
}
