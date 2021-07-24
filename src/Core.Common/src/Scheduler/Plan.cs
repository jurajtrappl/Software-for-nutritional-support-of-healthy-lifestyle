using Application.Core.Common.Entities.Base;
using Application.Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Common.Scheduler
{
    /// <summary>
    /// Data holder for scheduled plan items.
    /// </summary>
    /// <typeparam name="TScheduled">type of scheduled items.</typeparam>
    public sealed class Plan<TScheduled> : Dictionary<DateTime, TScheduled> where TScheduled : IPlanItem
    {
        /// <summary>
        /// Indicates whether there is a scheduled item(s) on the given date.
        /// </summary>
        /// <param name="date">date to check.</param>
        /// <returns>True if there is a plan on the <paramref name="date" />; othetwise False.</returns>
        public bool HasPlanOnDate(DateTime date) => Keys.Any(key => key.IsTheSameDateAs(date));

        /// <summary>
        /// Returns the scheduled item satisfying the <seealso cref="DateTime" /> filter./&gt;.
        /// </summary>
        /// <param name="filter">filtering condition.</param>
        /// <returns>Scheduled item if there is one; otherwise null.</returns>
        public TScheduled? GetItemOnDate(Func<DateTime, bool> filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return
                (from k in this
                 where filter(k.Key)
                 select k.Value).FirstOrDefault();
        }

        /// <summary>
        /// Returns sequence of scheduled items on the given date.
        /// </summary>
        /// <param name="date">date to check for items.</param>
        /// <returns>Sequence of scheduled items on <paramref name="date" />.</returns>
        public IEnumerable<TScheduled> GetOnlyItemsOnDate(DateTime date)
        {
            return
                from k in this
                where k.Key.IsTheSameDateAs(date)
                select k.Value;
        }

        /// <summary>
        /// Returns sequence of scheduled items with their dates on the given date.
        /// </summary>
        /// <param name="date">date to check for items.</param>
        /// <returns>Sequnce of scheduled items with their dates on <paramref name="date" />.</returns>
        public IEnumerable<KeyValuePair<DateTime, TScheduled>> GetItemsWithDatesOnDate(DateTime date)
        {
            return from k in this
                   where k.Key.IsTheSameDateAs(date)
                   select k;
        }
    }
}