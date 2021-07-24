using System;
using System.Collections.Generic;

namespace Application.Web.Areas.App.Calendar.ViewModels
{
    /// <summary>
    /// View model for month view option of the calendar.
    /// </summary>
    public sealed class CalendarMonthViewModel
    {
        /// <summary>
        /// Gets or initializes months names.
        /// </summary>
        public List<string> MonthsNames { get; init; }

        /// <summary>
        /// Gets or initializes week day names.
        /// </summary>
        public List<string> WeekDaysNames { get; init; }

        /// <summary>
        /// Gets or initializes month number.
        /// </summary>
        public int MonthNum { get; init; }

        /// <summary>
        /// Gets or initializes year.
        /// </summary>
        public int Year { get; init; }

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
        public IEnumerable<DateTime>? ExercisePlanDates { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="CalendarMonthViewModel"/>.
        /// </summary>
        public CalendarMonthViewModel()
        {
            MonthsNames = new();
            WeekDaysNames = new();
            Dates = new();
            MealPlanDates = new List<DateTime>();
            ExercisePlanDates = new List<DateTime>();
        }
    }
}