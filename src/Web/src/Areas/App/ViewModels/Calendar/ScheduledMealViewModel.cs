using Application.Core.Common.Entities;
using System.Collections.Generic;

namespace Application.Web.Areas.App.ViewModels.Calendar
{
    /// <summary>
    /// View model to display content of scheduled meal item.
    /// </summary>
    public sealed class ScheduledMealViewModel
    {
        /// <summary>
        /// Gets or initializes meal data.
        /// </summary>
        public Dictionary<string, IScheduledMeal> MealsData { get; init; }

        /// <summary>
        /// Gets or initializes a list of column names in the displayed modal.
        /// </summary>
        public List<string> ColNames { get; init; }

        /// <summary>
        /// Gets or initializes a formatted date by the specific culture.
        /// </summary>
        public string CultureFormattedDate { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="ScheduledMealViewModel"/>.
        /// </summary>
        public ScheduledMealViewModel()
        {
            MealsData = new();
            ColNames = new();
            CultureFormattedDate = string.Empty;
        }
    }
}
