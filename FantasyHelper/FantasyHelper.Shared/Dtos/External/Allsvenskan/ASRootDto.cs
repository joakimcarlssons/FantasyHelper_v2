using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos.External.Allsvenskan
{
    public class ASRootDto
    {
        [JsonPropertyName("events")]
        public List<ASGameweekDto>? Gameweeks { get; set; }

        [JsonPropertyName("teams")]
        public List<ASTeamDto>? Teams { get; set; }

        [JsonPropertyName("elements")]
        public List<ASPlayerDto>? Players { get; set; }
    }
}
