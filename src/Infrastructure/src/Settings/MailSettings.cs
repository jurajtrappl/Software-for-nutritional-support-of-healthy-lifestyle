namespace Application.Infrastructure.Settings
{
    /// <summary>
    /// Describes section of application settings file that is used for mail settings configuration.
    /// </summary>
    public class MailSettings
    {
        /// <summary>
        /// Gets or sets email address of sender.
        /// </summary>
        public string Mail { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets display name.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets password to senders email account.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets host.
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets use ssl indicator.
        /// </summary>
        public bool UseSSL { get; set; }
    }
}