

namespace FantasyHelper.Shared.Config
{
    public class FPLOptions
    {
        public const string Key = "FPL";
        public string RootEndpoint { get; set; } = default!;
        public string FixturesEndpoint { get; set; } = default!;
        public string LeagueEndpoint { get; set; } = default!;
        public string PriceFunctionEndpoint { get; set; } = default!;
        public string FunctionsKey { get; set; } = default!; 
        public int LoadingInterval { get; set; }
        public bool ShouldLoad { get; set; }
    }
}
