using Application.Core.Common.NutritionalParameters;
using Application.Core.Nutrition.EatingOccasions.Base;
using Application.Core.Nutrition.EatingOccasions.FiveMeals;
using Application.Core.Nutrition.Frequencies.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.Frequencies
{
    /// <summary>
    /// Contains data when eating five times a day.
    /// </summary>
    internal sealed class FiveMeals : MealFrequency
    {
        /// <summary>
        /// List of meal occasions eaten per day when eating five times a day.
        /// </summary>
        internal override IReadOnlyList<MealOccasion> Meals =>
            new List<MealOccasion>()
            {
                new FiveMealsBreakfast(),
                new FiveMealsMidMorningSnack(),
                new FiveMealsLunch(),
                new FiveMealsAfternoonSnack(),
                new FiveMealsDinner()
            };

        /// <summary>
        /// Indicates whether the given total daily expenditure fits five meals frequency.
        /// </summary>
        /// <param name="tee"><inheritdoc /></param>
        /// <returns>True if <paramref name="tee" /> suits; otherwise False.</returns>
        internal override bool Fits(TotalDailyEnergyExpenditure tee) => tee.IsAverage();
    }
}