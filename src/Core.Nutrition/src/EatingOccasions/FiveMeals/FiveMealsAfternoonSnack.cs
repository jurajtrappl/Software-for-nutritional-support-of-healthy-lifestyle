using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.EatingOccasions.Base;

namespace Application.Core.Nutrition.EatingOccasions.FiveMeals
{
    /// <summary>
    /// Afternoon snack meal occasion when eating five times a day.
    /// </summary>
    internal sealed class FiveMealsAfternoonSnack : AfternoonSnack
    {
        /// <summary>
        /// Afternoon snack proportion to the total daily requirement when eating five times a day.
        /// </summary>
        public override double TeeRatio => MealOccasionRatios.FiveMealsAfternoonSnackRatio;
    }
}