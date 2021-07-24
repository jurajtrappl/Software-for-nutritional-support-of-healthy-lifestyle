namespace Application.Web.Areas.App.ViewModels.Calendar
{
    /// <summary>
    /// View model to display content of scheduled drinking regime item.
    /// </summary>
    public sealed class ScheduledDrinkViewModel
    {
        /// <summary>
        /// Gets or initializes a scheduled drinking regime.
        /// </summary>
        public double Amount { get; init; }

        /// <summary>
        /// Gets or initializes a formatted date by the specific culture.
        /// </summary>
        public string CultureFormattedDate { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="ScheduledDrinkViewModel"/>.
        /// </summary>
        public ScheduledDrinkViewModel()
        {
            CultureFormattedDate = string.Empty;
        }
    }
}
