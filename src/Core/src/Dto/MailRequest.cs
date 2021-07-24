namespace Application.Core.Dto
{
    /// <summary>
    /// Template for an email.
    /// </summary>
    public sealed class MailRequest
    {
        /// <summary>
        /// Get or initializes recipient address.
        /// </summary>
        public string ToEmail { get; init; }

        /// <summary>
        /// Gets or initializes subject.
        /// </summary>
        public string Subject { get; init; }

        /// <summary>
        /// Gets or initializes body.
        /// </summary>
        public string Body { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="MailRequest" /> with the given recipient address, subject and body.
        /// </summary>
        /// <param name="toEmail">recipient address.</param>
        /// <param name="subject">subject of the email.</param>
        /// <param name="body">body of the email.</param>
        public MailRequest(string toEmail, string subject, string body)
        {
            (ToEmail, Subject, Body) = (toEmail, subject, body);
        }
    }
}