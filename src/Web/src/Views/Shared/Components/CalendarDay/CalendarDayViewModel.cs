using Application.Infrastructure.Entities;
using System;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for calendar day component.
    /// </summary>
    public sealed class CalendarDayViewModel
    {
        /// <summary>
        /// Gets or initializes date.
        /// </summary>
        public DateTime Date { get; init; }

        /// <summary>
        /// Gets or initializes date as formatted string.
        /// </summary>
        public string DateAsKey { get; init; }

        /// <summary>
        /// Gets or initializes user.
        /// </summary>
        public ApplicationUser User { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="CalendarDayViewModel"/>.
        /// </summary>
        /// <param name="date">date to display.</param>
        /// <param name="dateAsKey">date in localized format.</param>
        /// <param name="user">user for whom is the component being displayed.</param>
        public CalendarDayViewModel(DateTime date, string dateAsKey, ApplicationUser user)
        {
            (Date, DateAsKey, User) = (date, dateAsKey, user);
        }
    }
}