namespace SchedulePath.Services
{
    public interface IMailManager
    {
        void SendEmailAsync(string email, string subject, string message);
    }
}
