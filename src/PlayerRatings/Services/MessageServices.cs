
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.OptionsModel;
using MimeKit;

namespace PlayerRatings.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly IOptions<AppSettings> _settings;

        public AuthMessageSender(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Player Ratings", "ratingsplayer@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            if (string.IsNullOrEmpty(_settings.Value.SmtpUserName) ||
                string.IsNullOrEmpty(_settings.Value.SmtpPassword))
            {
                return;
            }

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.sendgrid.net", 587, false);

                client.Authenticate(_settings.Value.SmtpUserName, _settings.Value.SmtpPassword);

                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
