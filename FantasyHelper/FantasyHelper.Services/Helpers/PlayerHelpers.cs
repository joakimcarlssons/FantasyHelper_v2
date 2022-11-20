using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FantasyHelper.Services.Helpers
{
    public static class PlayerHelpers
    {
        public static async Task<IEnumerable<Player>> GetFPLPlayersClosestToPriceRise(this IRepository db, string endpoint)
        {
            try
            {
                var players = await FetchFPLPriceRisePlayers(endpoint, 3, TimeSpan.FromSeconds(5));
                if (!players.Any()) throw new Exception("No players fetched.");
                return ParseFetchedFPLPricePlayers(players, db);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<IEnumerable<Player>> GetFPLPlayersClosestToPriceFall(this IRepository db, string endpoint)
        {
            try
            {
                var players = await FetchFPLPriceFallPlayers(endpoint, 3, TimeSpan.FromSeconds(5));
                if (!players.Any()) throw new Exception("No players fetched.");
                return ParseFetchedFPLPricePlayers(players, db);
            }
            catch
            {
                throw;
            }
        }

        private static async ValueTask<IEnumerable<string>> FetchFPLPriceRisePlayers(string endpoint, int numberOfRetries, TimeSpan retryDelay)
        {
            var options = new ChromeOptions();
            var driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options, TimeSpan.FromSeconds(300));

            try
            {
                var retry = 0;
                while (retry <= numberOfRetries)
                {
                    driver.Navigate().GoToUrl(endpoint);
                    var players = driver.FindElement(By.XPath("//table/tbody")).Text.Split("\r\n");

                    if (players is null || players.Length == 0)
                    {
                        await Task.Delay(retryDelay);
                        retry++;
                    }
                    else return players;
                }

                throw new Exception("Maximum number of retries reached. No players were fetched.");
            }
            catch
            {
                throw;
            }
            finally
            {
                driver.Quit();
            }
        }

        private static async ValueTask<IEnumerable<string>> FetchFPLPriceFallPlayers(string endpoint, int numberOfRetries, TimeSpan retryDelay)
        {
            var options = new ChromeOptions();
            var driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options, TimeSpan.FromSeconds(300));

            try
            {
                var retry = 0;
                while (retry <= numberOfRetries)
                {
                    driver.Navigate().GoToUrl(endpoint);
                    driver.FindElement(By.ClassName("last")).Click();

                    var players = driver.FindElement(By.XPath("//table/tbody")).Text.Split("\r\n");

                    if (players is null || players.Length == 0)
                    {
                        await Task.Delay(retryDelay);
                        retry++;
                    }
                    else return players;
                }

                throw new Exception("Maximum number of retries reached. No players were fetched.");
            }
            catch
            {
                throw;
            }
            finally
            {
                driver.Quit();
            }
        }

        private static IEnumerable<Player> ParseFetchedFPLPricePlayers(IEnumerable<string> players, IRepository db)
        {
            if (players is null || !players.Any()) throw new Exception("No players provided.");

            var result = new List<Player>();
            foreach (var player in players)
            {
                var props = player.Split(" ");
                var name = props[0];
                var team = props[1];
                var target = props.Last();

                var matchingPlayers = db.GetPlayers(p => p.DisplayName == name && (p.Team?.Name?.Contains(team) ?? false), includeTeam: true);
                var storedPlayer = matchingPlayers.FirstOrDefault();
                
                if (matchingPlayers.Count() > 1)
                {
                    storedPlayer = matchingPlayers.FirstOrDefault(p => (p.Team?.Name?.Contains(props[2]) ?? false));
                }

                if (storedPlayer is null) throw new NullReferenceException($"Could not find player with name {name} and team {team}"); 

                storedPlayer.PriceTarget = target;
                result.Add(storedPlayer);
            }

            return result;
        }
    }
}
