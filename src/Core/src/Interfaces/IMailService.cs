using Application.Core.Dto;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    /// <summary>
    /// Provides support for sending emails to users.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Sends an email described by the given <paramref name="mailRequest" />.
        /// </summary>
        /// <param name="mailRequest">
        /// contains required information about the recipient, the subject and the body of the email.
        /// </param>
        Task SendEmailAsync(MailRequest mailRequest);
    }
}