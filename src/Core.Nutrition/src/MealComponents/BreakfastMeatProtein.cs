using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.BreakfastMeatProteins)]
    internal sealed class BreakfastMeatProtein : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BreakfastMeatProtein" /> with macronutrient and maximum amount.
        /// </summary>
        public BreakfastMeatProtein() :
            base(MacroNutrient.Protein, MealComponentAllowedAmounts.BreakfastMeatProteins)
        {
        }
    }
}