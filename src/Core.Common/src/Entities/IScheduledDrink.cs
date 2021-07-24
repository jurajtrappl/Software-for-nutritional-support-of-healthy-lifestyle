using Application.Core.Common.Entities.Base;

namespace Application.Core.Common.Entities
{
    /// <summary>
    /// An entity describing generated item in the drinking regime plan.
    /// </summary>
    public interface IScheduledDrink : IPlanItem
    {
        /// <summary>
        /// Gets amount of water needed to drink during one day.
        /// Units: liter (l).
        /// </summary>
        double Amount { get; }
    }
}