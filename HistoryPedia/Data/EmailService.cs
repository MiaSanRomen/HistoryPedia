using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace HistoryPedia.Data
{
    
    public class EmailService
    {
        public async Task SendEmailAsync(string login, string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("HistoryPedia", "romanusaug@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(login, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("romanusaug@gmail.com", "MiaSanMia2020");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}