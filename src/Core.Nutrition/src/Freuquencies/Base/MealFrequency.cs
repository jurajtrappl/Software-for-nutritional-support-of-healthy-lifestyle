using Application.Core.Common.NutritionalParameters;
using Application.Core.Nutrition.EatingOccasions.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.Frequencies.Base
{
    /// <summary>
    /// Basic type for classes describing the types and number of meals eaten per day.
    /// </summary>
    internal abstract class MealFrequency
    {
        /// <summary>
        /// List of meal occasions eaten per day.
        /// </summary>
        internal abstract IReadOnlyList<MealOccasion> Meals { get; }

        /// <summary>
        /// Indicates whether the given total energy expenditure fits for frequency.
        /// </summary>
        /// <param name="tee">tee to check.</param>
        /// <returns>True if <paramref name="tee" /> suits; otherwise False.</returns>
        internal abstract bool Fits(TotalDailyEnergyExpenditure tee);
    }
}