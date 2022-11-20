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
                var risingPlayers = await _playersService.GetPlayersClosestToPriceRise();
                //var fallingPlayers = await _playersService.GetPlayersClosestToPriceFall();

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
                    Body = EmailHelpers.ConstructEmailBaseContent("Daily FPL Update", ConstructDailyEmailContent(risingPlayers, new List<PlayerPriceChangeDto>())),
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send daily email: {ex.Message}", ex.Message);
                throw;
            }
        }

        private static string ConstructDailyEmailContent(IEnumerable<PlayerPriceChangeDto> risingPlayers, IEnumerable<PlayerPriceChangeDto> fallingPlayers)
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
                    $"<td align=\"center\">{player.Price / 10}</td>" +
                    $"<td align=\"center\">{player.PriceTarget}</td>" +
                    $"</tr>";
            }

            content += "</table><br><br><p style=\"font-weight:bold;\">Players closest to a price drop</p>";
            content += "<table style=\"margin-bottom:20px;\">" +
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
                    $"<td align=\"center\">{player.Price / 10}</td>" +
                    $"<td align=\"center\">{player.PriceTarget}</td>" +
                    $"</tr>";
            }

            content += "</table><br>";

            return content;
        }

        public async Task SendDeadlineMail()
        {
            throw new NotImplementedException();
        }
    }
}
