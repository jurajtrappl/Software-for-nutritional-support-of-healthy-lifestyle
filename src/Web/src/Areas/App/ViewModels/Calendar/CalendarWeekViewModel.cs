using System;
using System.Collections.Generic;

namespace Application.Web.Areas.App.Calendar.ViewModels
{
    /// <summary>
    /// View model for week view option of the calendar.
    /// </summary>
    public sealed class CalendarWeekViewModel
    {
        /// <summary>
        /// Gets or initializes week day names.
        /// </summary>
        public List<string> WeekDaysNames { get; init; }

        /// <summary>
        /// Gets or initializes week start date.
        /// </summary>
        public string WeekStartDate { get; init; }

        /// <summary>
        /// Gets or initializes week end date.
        /// </summary>
        public string WeekEndDate { get; init; }

        /// <summary>
        /// Gets or initializes dates.
        /// </summary>
        public List<DateTime> Dates { get; init; }

        /// <summary>
        /// Gets or initializes meal plan dates.
        /// </summary>
        public IEnumerable<DateTime> MealPlanDates { get; init; }

        /// <summary>
        /// Gets or initializes exercise plan dates.
        /// </summary>
        public IEnumerable<KeyValuePair<DateTime, int>>? ExercisePlanDates { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="CalendarWeekViewModel"/>.
        /// </summary>
        public CalendarWeekViewModel()
        {
            WeekDaysNames = new();
            WeekStartDate = string.Empty;
            WeekEndDate = string.Empty;
            Dates = new();
            MealPlanDates = new List<DateTime>();
            ExercisePlanDates = new Dictionary<DateTime, int>();
        }
    }
}