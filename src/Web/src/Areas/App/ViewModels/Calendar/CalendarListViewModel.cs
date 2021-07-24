using System;
using System.Collections.Generic;

namespace Application.Web.Areas.App.Calendar.ViewModels
{
    /// <summary>
    /// View model for list view option of the calendar.
    /// </summary>
    public sealed class CalendarListViewModel
    {
        /// <summary>
        /// Gets or initializes dates.
        /// </summary>
        public List<DateTime> Dates { get; init; }

        /// <summary>
        /// Gets or initializes formatted dates by the specific culture.
        /// </summary>
        public IEnumerable<string> CultureFormattedDates { get; init; }

        /// <summary>
        /// Gets or initializes plan items.
        /// </summary>
        public List<OneDayPlanItemsViewModel> PlanItems { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="CalendarListViewModel"/>.
        /// </summary>
        public CalendarListViewModel()
        {
            Dates = new();
            CultureFormattedDates = new List<string>();
            PlanItems = new();
        }
    }
}
