using Application.Core.Common.Entities;
using Application.Core.Common.Interfaces.Base;
using System.Collections.Generic;

namespace Application.Core.Common.Interfaces
{
    /// <summary>
    /// Contract for meal plan schedulers.
    /// </summary>
    public interface IMealPlanScheduler : IPlanScheduler<IScheduledMeal>
    {
        /// <summary>
        /// Adds ingredients from database, user information and application plan needed for the scheduling process.
        /// </summary>
        /// <param name="ingredients">ingredients from the database.</param>
        /// <param name="measurement">measurement of the user.</param>
        /// <param name="profile">profile of the user.</param>
        /// <param name="appPlan">application plan of the user.</param>
        /// <returns>instance of <seealso cref="IMealPlanScheduler" />.</returns>
        IMealPlanScheduler Configure(
            IReadOnlyDictionary<string, List<IIngredient>> ingredients,
            IApplicationUser user);
    }
}