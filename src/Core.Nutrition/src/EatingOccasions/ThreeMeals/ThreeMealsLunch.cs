using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.EatingOccasions.Base;

namespace Application.Core.Nutrition.EatingOccasions.ThreeMeals
{
    /// <summary>
    /// Lunch meal occasion when eating three times a day.
    /// </summary>
    internal sealed class ThreeMealsLunch : Lunch
    {
        /// <summary>
        /// Lunch proportion to the total daily requirement when eating three times a day.
        /// </summary>
        public override double TeeRatio => MealOccasionRatios.ThreeMealsLunchRatio;
    }
}