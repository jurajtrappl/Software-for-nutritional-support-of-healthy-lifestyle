using System;
using System.Collections.Generic;

namespace Application.Web.Views.Shared.Components
{
    public sealed class CalendarWeekDaysViewModel
    {
        /// <summary>
        /// Gets or initializes formatter of week day name.
        /// </summary>
        public Func<string, string> Formatter { get; init; }

        /// <summary>
        /// Gets or initializes collection of translated week days names.
        /// </summary>
        public List<string> WeekDaysNames { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="CalendarWeekDaysViewModel"/>
        /// wieth the given formatter and week days names.
        /// </summary>
        /// <param name="formatter">formatter of week day name.</param>
        /// <param name="weekDaysNames">collection of translated week days names.</param>
        public CalendarWeekDaysViewModel(Func<string, string> formatter, List<string> weekDaysNames)
        {
            Formatter = formatter ?? (value => value);
            WeekDaysNames = weekDaysNames;
        }
    }
}