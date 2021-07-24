using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.EatingOccasions.Base;

namespace Application.Core.Nutrition.EatingOccasions.SixMeals
{
    /// <summary>
    /// Mid morning snack meal occasion when eating six times a day.
    /// </summary>
    internal sealed class SixMealsMidMorningSnack : MidMorningSnack
    {
        /// <summary>
        /// Mid morning snack proportion to the total daily requirement when eating six times a day.
        /// </summary>
        public override double TeeRatio => MealOccasionRatios.SixMealsMidMorningSnackRatio;
    }
}