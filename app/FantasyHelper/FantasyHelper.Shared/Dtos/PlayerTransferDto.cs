using System.Text.Json.Serialization;

namespace FantasyHelper.Shared.Dtos
{
    public class PlayerTransferDto
    {
        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("team_name")]
        public string? TeamName { get; set; }

        [JsonPropertyName("transfers_in")]
        public int TransfersIn { get; set; }

        [JsonPropertyName("transfers_out")]
        public int TransfersOut { get; set; }
    }

    public class TransferredPlayersDto
    {
        [JsonPropertyName("most_transferred_in")]
        public IEnumerable<PlayerTransferDto>? TransferredInPlayers { get; set; }

        [JsonPropertyName("most_transferred_out")]
        public IEnumerable<PlayerTransferDto>? TransferredOutPlayers { get; set; }

        public TransferredPlayersDto(IEnumerable<PlayerTransferDto>? transferredInPlayers, IEnumerable<PlayerTransferDto>? transferredOutPlayers)
        {
            TransferredInPlayers = transferredInPlayers;
            TransferredOutPlayers = transferredOutPlayers;
        }
    }
}
