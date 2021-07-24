using Application.Core.Common.Entities;

namespace Application.Core.Nutrition.Schedulers
{
    /// <summary>
    /// Represents a scheduled drinking plan item.
    /// </summary>
    public sealed class ScheduledDrink : IScheduledDrink
    {
        /// <summary>
        /// Gets or initializes amount.
        /// Unit: liter.
        /// </summary>
        public double Amount { get; init; }
    }
}