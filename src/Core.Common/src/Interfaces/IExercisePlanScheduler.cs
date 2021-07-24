using Application.Core.Common.Entities;
using Application.Core.Common.Interfaces.Base;
using System.Collections.Generic;

namespace Application.Core.Common.Interfaces
{
    /// <summary>
    /// Contract for exercise plan schedulers.
    /// </summary>
    public interface IExercisePlanScheduler : IPlanScheduler<IScheduledExercise>
    {
        /// <summary>
        /// Adds weight data and exercises needed for the scheduling process.
        /// </summary>
        /// <param name="weight">weight of the user for whom is the plan being scheduled.</param>
        /// <param name="exercises">available exercises from the database.</param>
        /// <returns>instance of <seealso cref="IExercisePlanScheduler" />.</returns>
        IExercisePlanScheduler Configure(double weight, IReadOnlyList<IExercise> exercises);
    }
}