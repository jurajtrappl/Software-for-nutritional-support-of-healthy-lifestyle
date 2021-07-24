using Application.Core.Common.NutritionalParameters;
using Application.Core.Nutrition.EatingOccasions.Base;
using Application.Core.Nutrition.EatingOccasions.SixMeals;
using Application.Core.Nutrition.Frequencies.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.Frequencies
{
    /// <summary>
    /// Contains data when eating six times a day.
    /// </summary>
    internal sealed class SixMeals : MealFrequency
    {
        /// <summary>
        /// List of meal occasions eaten per day when eating six times a day.
        /// </summary>
        internal override IReadOnlyList<MealOccasion> Meals =>
            new List<MealOccasion>()
            {
                new SixMealsBreakfast(),
                new SixMealsMidMorningSnack(),
                new SixMealsLunch(),
                new SixMealsAfternoonSnack(),
                new SixMealsDinner(),
                new SixMealsSupper()
            };

        /// <summary>
        /// Indicates whether the given total daily expenditure fits six meals frequency.
        /// </summary>
        /// <param name="tee"><inheritdoc /></param>
        /// <returns>True if <paramref name="tee" /> suits; otherwise False.</returns>
        internal override bool Fits(TotalDailyEnergyExpenditure tee) => tee.IsAboveAverage();
    }
}