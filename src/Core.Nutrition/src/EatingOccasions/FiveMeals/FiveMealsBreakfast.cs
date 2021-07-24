using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.EatingOccasions.Base;

namespace Application.Core.Nutrition.EatingOccasions.FiveMeals
{
    /// <summary>
    /// Breakfast meal occasion when eating five times a day.
    /// </summary>
    internal sealed class FiveMealsBreakfast : Breakfast
    {
        /// <summary>
        /// Breakfast proportion to the total daily requirement when eating fives times a day.
        /// </summary>
        public override double TeeRatio => MealOccasionRatios.FiveMealsBreakfastRatio;
    }
}