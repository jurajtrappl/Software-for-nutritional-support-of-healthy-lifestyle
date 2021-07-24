using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    /// <summary>
    /// </summary>
    [CollectionName(MealCollectionNames.BreakfastEggProteins)]
    internal sealed class BreakfastEggProtein : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BreakfastEggProtein" /> with macronutrient and maximum amount.
        /// </summary>
        public BreakfastEggProtein() :
            base(MacroNutrient.Protein, MealComponentAllowedAmounts.BreakfastEggProteins)
        {
        }
    }
}