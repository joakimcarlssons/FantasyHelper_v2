using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos.External.Allsvenskan
{
    public class ASGameweekDto
    {
        [JsonPropertyName("id")]
        public int Gameweekid { get; set; }

        [JsonPropertyName("finished")]
        public bool IsFinished { get; set; }

        [JsonPropertyName("is_current")]
        public bool IsCurrent { get; set; }

        [JsonPropertyName("is_next")]
        public bool IsNext { get; set; }

        [JsonPropertyName("deadline_time")]
        public DateTime Deadline { get; set; }
    }
}
