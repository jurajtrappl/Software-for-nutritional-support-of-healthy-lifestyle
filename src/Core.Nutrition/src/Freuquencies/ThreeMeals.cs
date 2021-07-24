using Application.Core.Common.NutritionalParameters;
using Application.Core.Nutrition.EatingOccasions.Base;
using Application.Core.Nutrition.EatingOccasions.ThreeMeals;
using Application.Core.Nutrition.Frequencies.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.Frequencies
{
    /// <summary>
    /// Contains data when eating three times a day.
    /// </summary>
    internal sealed class ThreeMeals : MealFrequency
    {
        /// <summary>
        /// List of meal occasions eaten per day when eating three times a day.
        /// </summary>
        internal override IReadOnlyList<MealOccasion> Meals =>
            new List<MealOccasion>()
            {
                new ThreeMealsBreakfast(),
                new ThreeMealsLunch(),
                new ThreeMealsDinner()
            };

        /// <summary>
        /// Indicates whether the given total daily expenditure fits three meals frequency.
        /// </summary>
        /// <param name="tee"><inheritdoc /></param>
        /// <returns>True if <paramref name="tee" /> suits; otherwise False.</returns>
        internal override bool Fits(TotalDailyEnergyExpenditure tee) => tee.IsBelowAverage();
    }
}