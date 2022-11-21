namespace FantasyHelper.Shared.Dtos
{
    public class PlayerNewsDto
    {
        public string? DisplayName { get; set; }
        public string? TeamName { get; set; }
        public string? SelectedByPercent { get; set; }
        public string? News { get; set; }
        public DateTime? NewsAdded { get; set; }
    }
}
