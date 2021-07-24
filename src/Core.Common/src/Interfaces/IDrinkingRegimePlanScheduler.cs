using Application.Core.Common.Entities;
using Application.Core.Common.Interfaces.Base;

namespace Application.Core.Common.Interfaces
{
    /// <summary>
    /// Contract for drinking regime schedulers.
    /// </summary>
    public interface IDrinkingRegimePlanScheduler : IPlanScheduler<IScheduledDrink>
    {
        /// <summary>
        /// Adds weight data needed for the scheduling process.
        /// </summary>
        /// <param name="weight">weight of the user for whom is the plan being scheduled.</param>
        /// <returns>instance of <seealso cref="IDrinkingRegimePlanScheduler" />.</returns>
        IDrinkingRegimePlanScheduler Configure(double weight);
    }
}