namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for temp data message component.
    /// </summary>
    public sealed class TempDataMessageViewModel
    {
        /// <summary>
        /// Gets or initializes message,
        /// </summary>
        public string Message { get; init; }

        /// <summary>
        /// Gets or initializes css class of status color.
        /// </summary>
        public string StatusColor { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="TempDataMessageViewModel"/>
        /// with the given message and status color.
        /// </summary>
        /// <param name="message">message to display.</param>
        /// <param name="statusColor">css color of the message.</param>
        public TempDataMessageViewModel(string message, string statusColor)
        {
            StatusColor = statusColor;
            Message = message ?? string.Empty;
        }
    }
}