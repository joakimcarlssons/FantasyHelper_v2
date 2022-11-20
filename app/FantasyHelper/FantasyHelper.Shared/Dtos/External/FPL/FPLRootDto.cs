using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos.External.FPL
{
    public class FPLRootDto
    {
        [JsonPropertyName("events")]
        public List<FPLGameweekDto> Gameweeks { get; set; } = default!;

        [JsonPropertyName("teams")]
        public List<FPLTeamDto> Teams { get; set; } = default!;

        [JsonPropertyName("elements")]
        public List<FPLPlayerDto> Players { get; set; } = default!;
    }
}
