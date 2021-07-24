using Application.Core.Common.Entities;
using Application.Core.Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Application.Core.Services.Base
{
    /// <summary>
    /// Base class for assignees of scheduled items.
    /// </summary>
    internal abstract class ScheduledItemsDisplayer
    {
        /// <summary>
        /// Base method that sets scheduled items (every plan has drinking regime and meal plan).
        /// </summary>
        /// <param name="user"></param>
        /// <param name="date"></param>
        /// <param name="viewBag">view data holder.</param>
        internal virtual void SetItems(IApplicationUser user, DateTime date, dynamic viewBag)
        {
            GetDrinkingRegime(user, date, viewBag);
            GetMealPlan(user, date, viewBag);
        }

        /// <summary>
        /// Sets scheduled items of drinking regime plan on the given date to the view data holder.
        /// </summary>
        /// <param name="user">user who has scheduled plan.</param>
        /// <param name="date">date on which are being searched scheduled items.</param>
        /// <param name="viewBag">view data holder.</param>
        protected void GetDrinkingRegime(IApplicationUser user, DateTime date, dynamic viewBag)
        {
            viewBag.DrinkingRegime = user.DrinkingRegime.GetOnlyItemsOnDate(date).FirstOrDefault();
        }

        /// <summary>
        /// Sets scheduled items of exercise plan on the given date to the view data holder.
        /// </summary>
        /// <param name="user">user who has scheduled plan.</param>
        /// <param name="date">date on which are being searched scheduled items.</param>
        /// <param name="viewBag">view data holder.</param>
        protected void GetExercisePlan(IApplicationUser user, DateTime date, dynamic viewBag)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Exercises is null)
            {
                throw new NullReferenceException(nameof(user.Exercises));
            }

            var items = user.Exercises.GetItemsWithDatesOnDate(date)
                .ToDictionary(key => key.Key, value => (IPlanItem)value.Value);
            AddScheduledItemsToViewBag(items, viewBag);
        }

        /// <summary>
        /// Sets scheduled items of meal plan on the given date to the view data holder.
        /// </summary>
        /// <param name="user">user who has scheduled plan.</param>
        /// <param name="date">date on which are being searched scheduled items.</param>
        /// <param name="viewBag">view data holder.</param>
        protected void GetMealPlan(IApplicationUser user, DateTime date, dynamic viewBag)
        {
            var items = user.Meals.GetItemsWithDatesOnDate(date)
                .ToDictionary(key => key.Key, value => (IPlanItem)value.Value);
            AddScheduledItemsToViewBag(items, viewBag);
        }

        /// <summary>
        /// Assigns scheduled items to view data holder.
        /// </summary>
        /// <param name="items">items to assign.</param>
        /// <param name="viewBag">view data holder where items are assigned.</param>
        private static void AddScheduledItemsToViewBag(Dictionary<DateTime, IPlanItem> items, dynamic viewBag)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (viewBag.PlanItems is null)
            {
                viewBag.PlanItems = items.ToImmutableSortedDictionary();
            }
            else
            {
                viewBag.PlanItems = viewBag.PlanItems.AddRange(items);
            }
        }
    }
}