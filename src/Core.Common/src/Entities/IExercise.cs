using Application.Core.Common.Entities.Base;

namespace Application.Core.Common.Entities
{
    /// <summary>
    /// An entity describing an exercise activity.
    /// </summary>
    public interface IExercise : IDatabaseItem
    {
        /// <summary>
        /// Gets expended kilojoules per minute per kilogram of body weight exercising.
        /// </summary>
        double KjPerKgPerMin { get; }

        /// <summary>
        /// Gets the name of the exercise.
        /// </summary>
        string Name { get; }
    }
}