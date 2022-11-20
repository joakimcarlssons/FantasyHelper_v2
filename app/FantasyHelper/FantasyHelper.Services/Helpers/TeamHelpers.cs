using HtmlAgilityPack;

namespace FantasyHelper.Services.Helpers
{
    public static class TeamHelpers
    {
        /// <summary>
        /// Table holding name representations from different sources to map data
        /// </summary>
        private static readonly Hashtable MappedTeamNames = new()
        {
            { "Man City", "Manchester City FC" },
            { "Man Utd", "Manchester United FC" },
            { "Spurs", "Tottenham Hotspur FC" },
            { "Nott'm Forest", "Nottingham Forest FC" },
            { "Wolves", "Wolverhampton Wanderers FC" }
        };

        public static async Task AddLeagueTableData(this IEnumerable<Team> teamsToUpdate, HttpClient client, string leagueEndpoint, CancellationToken cancellationToken)
        {
            try
            {
                var response = await client.GetAsync(leagueEndpoint, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    // Parse HTML response
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(await response.Content.ReadAsStringAsync(cancellationToken));

                    var teams = htmlDoc.DocumentNode.Descendants("table")?
                        .FirstOrDefault(node => node.GetAttributeValue("class", "")
                        .Contains("full-league-table"))?
                        .Descendants("tbody")
                        .FirstOrDefault()?
                        .Descendants("tr")
                        .ToList();

                    if (teams is null) throw new NullReferenceException("No teams were found.");

                    foreach (var teamNode in teams)
                    {
                        var teamName = teamNode.Descendants("td")?
                            .FirstOrDefault(node => node.GetAttributeValue("class", "").Contains("team"))?
                            .Descendants("a").FirstOrDefault()?.InnerText;

                        if (String.IsNullOrEmpty(teamName)) throw new NullReferenceException("Could not parse name of extracted team.");

                        var teamToUpdate = teamsToUpdate.FirstOrDefault(team =>
                        {
                            if (String.IsNullOrEmpty(team.Name)) return false;
                            else
                            {
                                var nameToCheck = team.Name;
                                if (MappedTeamNames.ContainsKey(team.Name)) nameToCheck = MappedTeamNames[team.Name]?.ToString() ?? team.Name;
                                return teamName.Contains(nameToCheck);
                            }
                        });

                        if (teamToUpdate is null) throw new NullReferenceException($"Could not find any team with name {teamName}");

                        // Map properties
                        teamToUpdate.Position = int.Parse(teamNode.Descendants("td")?
                            .FirstOrDefault(node => node.GetAttributeValue("class", "").Contains("position"))?
                            .Descendants("span").FirstOrDefault()?.InnerText ?? "0");
                        teamToUpdate.MatchesPlayed = int.Parse(teamNode.SelectSingleNode(".//td[@class='mp']").InnerText);
                        teamToUpdate.Wins = int.Parse(teamNode.SelectSingleNode(".//td[@class='win']").InnerText);
                        teamToUpdate.Draws = int.Parse(teamNode.SelectSingleNode(".//td[@class='draw']").InnerText);
                        teamToUpdate.Losses = int.Parse(teamNode.SelectSingleNode(".//td[@class='loss']").InnerText);
                        teamToUpdate.GoalsScored = int.Parse(teamNode.SelectSingleNode(".//td[@class='gf']").InnerText);
                        teamToUpdate.GoalsConceded = int.Parse(teamNode.SelectSingleNode(".//td[@class='ga']").InnerText);
                        teamToUpdate.GoalDifference = int.Parse(teamNode.SelectSingleNode(".//td[@class='gd']").InnerText);
                        teamToUpdate.Points = int.Parse(teamNode.Descendants("td")?
                            .FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Contains("points"))?.InnerText ?? "0");
                    }
                }
                else throw new Exception($"Failed to load league table data. Response was of status code {response.StatusCode}.");
            }
            catch
            {
                throw;
            }
        }
    }
}
