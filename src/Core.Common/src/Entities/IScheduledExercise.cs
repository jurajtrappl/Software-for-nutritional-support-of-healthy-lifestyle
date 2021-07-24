using Application.Core.Common.Entities.Base;
using Application.Core.Common.Enums;

namespace Application.Core.Common.Entities
{
    /// <summary>
    /// An entity describing generated item in the exercise plan.
    /// </summary>
    public interface IScheduledExercise : IPlanItem
    {
        /// <summary>
        /// Gets duration.
        /// Unit: minutes.
        /// </summary>
        int Duration { get; }

        /// <summary>
        /// Gets sport type.
        /// </summary>
        Sport Type { get; }
    }
}