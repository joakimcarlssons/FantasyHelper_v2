namespace FantasyHelper.Shared.Config
{
    public class AllsvenskanOptions
    {
        public const string Key = "Allsvenskan";
        public string RootEndpoint { get; set; } = default!;
        public string FixturesEndpoint { get; set; } = default!;
        public string LeagueEndpoint { get; set; } = default!;
        public string PriceEndpoint { get; set; } = default!;
        public int LoadingInterval { get; set; }
        public bool ShouldLoad { get; set; }
    }
}
