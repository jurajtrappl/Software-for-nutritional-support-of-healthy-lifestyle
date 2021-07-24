using Application.Core.Attributes;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Services.Base;
using System;

namespace Application.Core.Services.LogicProviders
{
    /// <summary>
    /// Provides logic for displaying scheduled items for user with reduce application plan.
    /// </summary>
    [ForPlan(ApplicationPlan.Reduce)]
    internal sealed class ReducePlanDisplayerService : ScheduledItemsDisplayer
    {
        /// <summary>
        /// Sets scheduled items of reduce application plan to the given view data holder for the given user on the
        /// given date.
        /// </summary>
        /// <param name="user">user for whom the items are being set.</param>
        /// <param name="date">date of the plans.</param>
        /// <param name="viewBag">view data holder.</param>
        internal override void SetItems(IApplicationUser user, DateTime date, dynamic viewBag)
        {
            GetDrinkingRegime(user, date, viewBag);
            GetExercisePlan(user, date, viewBag);
            GetMealPlan(user, date, viewBag);
        }
    }
}