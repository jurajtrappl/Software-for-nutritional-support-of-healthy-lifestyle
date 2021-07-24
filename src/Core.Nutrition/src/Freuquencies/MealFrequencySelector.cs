using Application.Core.Common.Enums;
using Application.Core.Common.NutritionalParameters;
using Application.Core.Nutrition.EatingOccasions.Base;
using Application.Core.Nutrition.Frequencies.Base;
using System;
using System.Collections.Generic;

namespace Application.Core.Nutrition.Frequencies
{
    /// <summary>
    /// Selects a suitable number of meals per day.
    /// </summary>
    internal static class MealFrequencySelector
    {
        /// <summary>
        /// List of frequencies.
        /// </summary>
        private static readonly List<MealFrequency> _frequencies =
            new()
            {
                new ThreeMeals(),
                new FiveMeals(),
                new SixMeals()
            };

        /// <summary>
        /// Sport plan has always six meals frequency, therefore pre-define this frequency alone.
        /// </summary>
        private static readonly IReadOnlyList<MealOccasion> _sixMeals = new SixMeals().Meals;

        /// <summary>
        /// Decides how many meals per day will be served based on total daily energy expenditure.
        /// </summary>
        /// <param name="appPlan">application plan of the user.</param>
        /// <param name="tee">total energy expenditure of the user.</param>
        /// <returns>Suitable meal frequency as <see cref="IReadOnlyList{MealOccasion}" /></returns>
        internal static IReadOnlyList<MealOccasion> Choose(ApplicationPlan appPlan, TotalDailyEnergyExpenditure tee)
        {
            if (appPlan == ApplicationPlan.Sport)
            {
                return _sixMeals;
            }

            MealFrequency? frequency = _frequencies.Find(f => f.Fits(tee));
            if (frequency is null)
            {
                throw new NullReferenceException(nameof(frequency));
            }

            return frequency.Meals;
        }
    }
}