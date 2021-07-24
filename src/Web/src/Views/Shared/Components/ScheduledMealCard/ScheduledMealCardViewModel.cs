namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for scheduled meal component.
    /// </summary>
    public class ScheduledMealCardViewModel
    {
        /// <summary>
        /// Gets or initializes translated meal name.
        /// </summary>
        public string TranslatedMealName { get; init; }

        /// <summary>
        /// Gets or initializes formatted datetime.
        /// </summary>
        public string FormattedDateAndTime { get; init; }

        /// <summary>
        /// Gets or initializes date formatted to insert to database.
        /// </summary>
        public string DatabaseKeyDate { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="ScheduledMealCardViewModel"/> with the given date as database key,
        /// formatted datetime and translated meal name.
        /// </summary>
        /// <param name="databaseKeyDate">date as database key.</param>
        /// <param name="formattedDateAndTime">formatted date and time.</param>
        /// <param name="translatedMealName">localized name of the meal.</param>
        public ScheduledMealCardViewModel(string databaseKeyDate, string formattedDateAndTime, string translatedMealName)
        {
            (TranslatedMealName, FormattedDateAndTime, DatabaseKeyDate) =
                (translatedMealName, formattedDateAndTime, databaseKeyDate);
        }
    }
}