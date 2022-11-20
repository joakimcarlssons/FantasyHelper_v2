using Quartz;

namespace FantasyHelper.Services.Scheduled
{
    public class SendDailyNotifications : IJob
    {
        private readonly ILogger<SendDailyNotifications> _logger;
        private readonly IEmailService _emailService;

        public SendDailyNotifications(ILogger<SendDailyNotifications> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("--> Sending daily email...");
                await _emailService.SendDailyMail();
                _logger.LogInformation("--> Daily email has been sent!");
            }
            catch (Exception ex)
            {
                _logger.LogError("--> Failed to execute sending of daily notification: {ex.Message}", ex.Message);
                throw;
            }
        }
    }
}
