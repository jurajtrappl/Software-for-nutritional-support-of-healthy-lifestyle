using Application.Core.Common.Dto;

namespace Application.Web.Areas.App.Models.Settings
{
    /// <summary>
    /// Model for all forms in App/Settings/Index view.
    /// </summary>
    public sealed class ChangeEatingTimeConfigModel
    {
        /// <summary>
        /// Gets or initializes breakfast time.
        /// </summary>
        public HourData Breakfast { get; init; } = new();

        /// <summary>
        /// Gets or initializes mid morning snack.
        /// </summary>
        public HourData MidMorningSnack { get; init; } = new();

        /// <summary>
        /// Gets or initializes lunch.
        /// </summary>
        public HourData Lunch { get; init; } = new();

        /// <summary>
        /// Gets or initializes afternoon snack.
        /// </summary>
        public HourData AfternoonSnack { get; init; } = new();

        /// <summary>
        /// Gets or initializes dinner.
        /// </summary>
        public HourData Dinner { get; init; } = new();

        /// <summary>
        /// Gets or initializes supper.
        /// </summary>
        public HourData Supper { get; init; } = new();
    }
}