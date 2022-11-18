namespace FantasyHelper.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendDailyMail();
        Task SendDeadlineMail();
    }
}
