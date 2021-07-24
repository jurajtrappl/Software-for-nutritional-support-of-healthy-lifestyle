using Application.Core.Dto;
using Application.Core.Interfaces;
using Application.Infrastructure.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace Application.Infrastructure.Services
{
    /// <summary>
    /// Implementation of infrastructure mail service.
    /// </summary>
    public sealed class MailService : IMailService
    {
        /// <summary>
        /// Application email settings.
        /// </summary>
        private readonly MailSettings _mailSettings;

        /// <summary>
        /// Initializes a new instance of <see cref="MailService" /> with the given application email settings.
        /// </summary>
        /// <param name="mailSettings"></param>
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        /// <summary>
        /// Sends an email described by the given <paramref name="mailRequest" />.
        /// </summary>
        /// <param name="mailRequest">
        /// contains required information about the recipient, the subject and the body of the email.
        /// </param>
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            MimeMessage email = new();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            BodyBuilder builder = new();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            await SendEmailViaSmtp(email);
        }

        /// <summary>
        /// Sends the given <see cref="MimeMessage" /> using <see cref="SmtpClient" />.
        /// </summary>
        /// <param name="email">email to send.</param>
        private async Task SendEmailViaSmtp(MimeMessage email)
        {
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}