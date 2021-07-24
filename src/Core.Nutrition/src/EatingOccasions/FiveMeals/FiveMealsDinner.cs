using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.EatingOccasions.Base;

namespace Application.Core.Nutrition.EatingOccasions.FiveMeals
{
    /// <summary>
    /// Dinner meal occasion when eating five times a day.
    /// </summary>
    internal sealed class FiveMealsDinner : Dinner
    {
        /// <summary>
        /// Dinner proportion to the total daily requirement when eating five times a day.
        /// </summary>
        public override double TeeRatio => MealOccasionRatios.FiveMealsDinnerRatio;
    }
}