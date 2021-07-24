using Application.Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for calendar component.
    /// </summary>
    public sealed class CalendarViewModel
    {
        /// <summary>
        /// Gets or initializes user.
        /// </summary>
        public ApplicationUser User { get; init; }

        /// <summary>
        /// Gets or initializes dates.
        /// </summary>
        public List<DateTime> Dates { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="CalendarViewModel"/>
        /// with the given user and dates.
        /// </summary>
        /// <param name="user">user for whom the calendar is being displayed.</param>
        /// <param name="dates">dates in the calendar component.</param>
        public CalendarViewModel(ApplicationUser user, List<DateTime> dates)
        {
            (User, Dates) = (user, dates);
        }
    }
}