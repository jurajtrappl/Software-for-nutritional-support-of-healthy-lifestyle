using Application.Core.Common.Entities;
using Application.Core.Common.Enums;

namespace Application.Core.Exercise.Schedulers
{
    /// <summary>
    /// Represents an exercise activity.
    /// </summary>
    public sealed class ScheduledExercise : IScheduledExercise
    {
        /// <summary>
        /// Gets or initializes a duration.
        /// </summary>
        public int Duration { get; init; }

        /// <summary>
        /// Gets or initializes a type of the activity.
        /// </summary>
        public Sport Type { get; init; }
    }
}