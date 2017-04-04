using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace SchedulePath.Services
{
    public class MailManager : IMailManager
    {
        public void SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Schedule Criticality", "schedule.criticality@cep.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };
            using (var client = new SmtpClient())
            {
                var credentials = new System.Net.NetworkCredential();
                credentials.UserName = "jzh.softdev@gmail.com";
                credentials.Password = "jzhsoftdev";
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(System.Text.Encoding.ASCII, credentials);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}