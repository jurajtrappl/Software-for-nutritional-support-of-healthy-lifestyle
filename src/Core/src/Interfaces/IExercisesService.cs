using Application.Core.Common.Entities;
using System.Collections.Generic;

namespace Application.Core.Interfaces
{
    /// <summary>
    /// Infrastructure service contract for exercise database.
    /// </summary>
    public interface IExercisesService
    {
        /// <summary>
        /// Gets all exercises that are available.
        /// </summary>
        /// <returns>All appropriate exercises from the database.</returns>
        IReadOnlyList<IExercise> GetAllExercises();
    }
}