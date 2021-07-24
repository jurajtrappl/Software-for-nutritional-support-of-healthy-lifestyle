using Application.Core.Common.Entities;
using System;

namespace Application.Core.Interfaces
{
    /// <summary>
    /// UI service contract that supports displaying appropriate scheduled items.
    /// </summary>
    public interface IScheduledItemsDisplayService
    {
        /// <summary>
        /// Finds scheduled plan items (from every scheduled plan [depending on the application plan]) on the given date
        /// and assign them to view data holder.
        /// </summary>
        /// <param name="user">the user whose plans are assigned.</param>
        /// <param name="date">date to check for scheduled plans.</param>
        /// <param name="viewBag">view data holder.</param>
        void SelectScheduledItemsForDate(IApplicationUser user, DateTime date, dynamic viewBag);
    }
}