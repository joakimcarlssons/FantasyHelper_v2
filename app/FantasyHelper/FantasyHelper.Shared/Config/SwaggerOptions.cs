namespace FantasyHelper.Shared.Config
{
    public class SwaggerOptions
    {
        public const string Key = "Swagger";
        public string JsonRoute { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string UIEndpoint { get; set; } = default!;
    }
}
