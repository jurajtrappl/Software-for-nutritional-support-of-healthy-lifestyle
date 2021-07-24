using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.BreakfastMilkProteins)]
    internal sealed class BreakfastMilkProtein : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BreakfastMilkProtein" /> with macronutrient and maximum amount.
        /// </summary>
        public BreakfastMilkProtein() :
            base(MacroNutrient.Protein, MealComponentAllowedAmounts.BreakfastMilkProteins)
        {
        }
    }
}