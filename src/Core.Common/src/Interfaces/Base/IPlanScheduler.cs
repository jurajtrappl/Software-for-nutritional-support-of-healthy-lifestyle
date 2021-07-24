using Application.Core.Common.Entities.Base;
using Application.Core.Common.Scheduler;

namespace Application.Core.Common.Interfaces.Base
{
    /// <summary>
    /// Contract for schedulers.
    /// </summary>
    /// <typeparam name="TScheduledPlanItem">type of the scheduled item.</typeparam>
    public interface IPlanScheduler<TScheduledPlanItem> where TScheduledPlanItem : IPlanItem
    {
        /// <summary>
        /// Prepares a plan with <see cref="TScheduledPlanItem" /> items with the specific length in days.
        /// </summary>
        /// <returns>scheduled items as <seealso cref="Plan{TScheduledPlanItem}" />.</returns>
        Plan<TScheduledPlanItem> Schedule();
    }
}