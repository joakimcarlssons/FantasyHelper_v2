namespace FantasyHelper.Services.FPL
{
    public class FPLEmailService : IEmailService
    {
        private readonly ILogger<FPLEmailService> _logger;
        private readonly EmailOptions _emailOptions;
        private readonly SmtpOptions _smtpOptions;
        private readonly IPlayersService _playersService;

        public FPLEmailService(ILogger<FPLEmailService> logger, IOptions<EmailOptions> emailOptions, IOptions<SmtpOptions> smtpOptions, IPlayersService playersService)
        {
            _logger = logger;
            _emailOptions = emailOptions.Value;
            _smtpOptions = smtpOptions.Value;
            _playersService = playersService;
        }

        public async Task SendDailyMail()
        {
            try
            {
                var priceChangingPlayers = await _playersService.GetPriceChangingPlayers();
                var newsPlayers = _playersService.GetPlayerNews(DateTime.Today.AddDays(-1)); // Get player news published from the day before

                var result = await EmailHelpers.SendEmail(new()
                {
                    Host = _smtpOptions.Host,
                    Port = _smtpOptions.Port,
                    Username = _smtpOptions.Username,
                    Password = _smtpOptions.Password,
                    SenderEmail = _emailOptions.SenderEmail,
                    SenderName = _emailOptions.SenderName,
                    ReceiverEmail = _emailOptions.ReceiverEmail,
                    ReceiverName = _emailOptions.ReceiverName,
                    Subject = "Daily FPL Update",
                    Body = EmailHelpers.ConstructEmailBaseContent("Daily FPL Update", 
                        ConstructDailyEmailContent(priceChangingPlayers.RisingPlayers!, priceChangingPlayers.FallingPlayers!, newsPlayers)),
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send daily email: {ex.Message}", ex.Message);
                throw;
            }
        }

        private static string ConstructDailyEmailContent(IEnumerable<PlayerPriceChangeDto> risingPlayers, IEnumerable<PlayerPriceChangeDto> fallingPlayers, IEnumerable<PlayerNewsDto> newsPlayers)
        {
            var content = "<p style=\"font-weight:bold;\">Players closest to a price rise</p>";

            content += "<table style=\"\">" +
                "<tr>" +
                "<th align=\"center\" style=\"margin:0 25px;\">Player</th>" +
                "<th align=\"center\" style=\"margin:0 25px;\">Club</th>" +
                "<th align=\"center\" style=\"margin:0 25px;\">Price</th>" +
                "<th align=\"center\" style=\"margin:0 25px;\">Target</th>" +
                "</tr>";

            foreach (var player in risingPlayers)
            {
                content += $"<tr>" +
                    $"<td align=\"center\">{player.DisplayName}</td>" +
                    $"<td align=\"center\">{player.TeamName}</td>" +
                    $"<td align=\"center\">{player.Price}</td>" +
                    $"<td align=\"center\">{player.PriceTarget}</td>" +
                    $"</tr>";
            }

            content += "</table><br><br><p style=\"font-weight:bold;\">Players closest to a price drop</p>";
            content += "<table style=\"margin:0 0 20px 0px;\">" +
                "<tr>" +
                "<th align=\"center\" style=\"margin:0 25px;\">Player</th>" +
                "<th align=\"center\" style=\"margin:0 25px;\">Club</th>" +
                "<th align=\"center\" style=\"margin:0 25px;\">Price</th>" +
                "<th align=\"center\" style=\"margin:0 25px;\">Target</th>" +
                "</tr>";

            foreach (var player in fallingPlayers)
            {
                content += $"<tr>" +
                    $"<td align=\"center\">{player.DisplayName}</td>" +
                    $"<td align=\"center\">{player.TeamName}</td>" +
                    $"<td align=\"center\">{player.Price}</td>" +
                    $"<td align=\"center\">{player.PriceTarget}</td>" +
                    $"</tr>";
            }

            content += "</table><br>";

            if (newsPlayers != null && newsPlayers.Any())
            {
                content += "</table><br><br><p style=\"font-weight:bold;\">Player news</p>";
                content += "<table style=\"margin:0 0 20px 0px;\">" +
                "<tr>" +
                "<th align=\"center\" style=\"margin:0 25px;\">Player</th>" +
                "<th align=\"center\" style=\"margin:0 25px;\">Club</th>" +
                "<th align=\"center\" style=\"margin:0 25px;\">News</th>" +
                "</tr>";

                foreach (var player in newsPlayers)
                {
                    content += $"<tr>" +
                    $"<td align=\"center\">{player.DisplayName}</td>" +
                    $"<td align=\"center\">{player.TeamName}</td>" +
                    $"<td align=\"center\">{player.News}</td>" +
                    $"</tr>";
                }

                content += "</table><br>";
            }

            return content;
        }

        public async Task SendDeadlineMail()
        {
            throw new NotImplementedException();
        }
    }
}
