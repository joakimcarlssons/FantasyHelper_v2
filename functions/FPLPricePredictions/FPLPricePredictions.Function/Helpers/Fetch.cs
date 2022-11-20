namespace FPLPricePredictions.Function.Helpers
{
    public static class Fetch
    {
        public static async Task<IEnumerable<PlayerPriceChangeDto>> RisingPlayers()
        {
            try
            {
                var browser = await SetupBrowser();
                using var page = await browser.NewPageAsync();
                await page.SetUserAgentAsync(Config.UserAgent);
                await page.GoToAsync(Config.URL);
                await page.WaitForNetworkIdleAsync();

                var content = await page.GetContentAsync();

                var context = BrowsingContext.New(Configuration.Default);
                var document = await context.OpenAsync(req => req.Content(content));

                var players = document.QuerySelector("#myDataTable")?.QuerySelector("tbody")?.QuerySelectorAll("tr");
                return ParsePlayers(players);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<IEnumerable<PlayerPriceChangeDto>> FallingPlayers()
        {
            var browser = await SetupBrowser();
            using var page = await browser.NewPageAsync();
            await page.SetUserAgentAsync(Config.UserAgent);
            await page.GoToAsync(Config.URL);
            await page.WaitForNetworkIdleAsync();

            await page.ClickAsync(".last");

            var content = await page.GetContentAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(content));

            var players = document.QuerySelector("#myDataTable")?.QuerySelector("tbody")?.QuerySelectorAll("tr");
            return ParsePlayers(players);
        }

        private static async Task<IBrowser> SetupBrowser()
        {
            var browserFetcher = new BrowserFetcher(new BrowserFetcherOptions
            {
                Path = Path.GetTempPath()
            });

            await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = browserFetcher.RevisionInfo(BrowserFetcher.DefaultChromiumRevision.ToString()).ExecutablePath
            });

            return browser;
        }

        private static IEnumerable<PlayerPriceChangeDto> ParsePlayers(IEnumerable<IElement> fetchedPlayers)
        {
            var parsedPlayers = new List<PlayerPriceChangeDto>();

            foreach (var player in fetchedPlayers)
            {
                var props = player.QuerySelectorAll("td");

                var t = props[6].InnerHtml[1..^1];

                parsedPlayers.Add(new()
                {
                    DisplayName = props[1].QuerySelector("span").InnerHtml,
                    TeamName = props[2].InnerHtml,
                    Price = decimal.Parse(props[6].InnerHtml[1..^1], CultureInfo.InvariantCulture),
                    PriceTarget = props[^2].InnerHtml
                });
            }

            return parsedPlayers;
        }
    }
}
