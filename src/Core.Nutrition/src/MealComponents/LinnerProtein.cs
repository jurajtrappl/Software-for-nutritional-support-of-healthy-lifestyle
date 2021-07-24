using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.LinnerProteins)]
    internal sealed class LinnerProtein : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LinnerProtein" /> with macronutrient and maximum amount.
        /// </summary>
        public LinnerProtein() : base(MacroNutrient.Protein, MealComponentAllowedAmounts.LinnerProteins)
        {
        }
    }
}