using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.PastaSideDishes)]
    internal sealed class PastaSideDish : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PastaSideDish" /> with macronutrient and maximum amount.
        /// </summary>
        public PastaSideDish() : base(MacroNutrient.Carbohydrate, MealComponentAllowedAmounts.PastaSideDishes)
        {
        }
    }
}