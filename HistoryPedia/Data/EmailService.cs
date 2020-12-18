using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace HistoryPedia.Data
{
    public interface IEmailService
    {
        Task SendEmailAsync(string login, string email, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly SmtpClient smtpClient = new SmtpClient();

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
            smtpClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, false);

            smtpClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
        }

        public async Task SendEmailAsync(string login, string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("HistoryPedia", _emailConfiguration.SmtpUsername));
            emailMessage.To.Add(new MailboxAddress(login, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            await smtpClient.SendAsync(emailMessage);

            //using (var client = new SmtpClient())
            //{
            //    await client.ConnectAsync("smtp.gmail.com", 465, true);
            //    await client.AuthenticateAsync("romanusaug@gmail.com", "MiaSanMia2020");
            //    await client.SendAsync(emailMessage);

            //    await client.DisconnectAsync(true);
            //}
        }
    }
}