namespace FantasyHelper.Shared.Dtos
{
    public class PlayerPriceChangeDto
    {
        public string? DisplayName { get; set; }
        public decimal? Price { get; set; }
        public string? PriceTarget { get; set; }
        public string? TeamName { get; set; }
    }
}
