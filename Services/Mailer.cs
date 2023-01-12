using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using TaskApp.Services.Interfaces;

namespace TaskApp.Services
{
    public class Mailer : IMailer
    {
        private readonly IConfiguration configuration;

        public Mailer(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task Send(string to, string subject, string html, string? from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration["Mailer:User"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new SmtpClient();
            smtp.Connect(
                configuration.GetSection("Mailer:Host").Value,
                Convert.ToInt32(configuration.GetSection("Mailer:Port").Value),
                SecureSocketOptions.StartTls
            );
            smtp.Authenticate(
                configuration.GetSection("Mailer:User").Value,
                configuration.GetSection("Mailer:Pass").Value
            );
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}