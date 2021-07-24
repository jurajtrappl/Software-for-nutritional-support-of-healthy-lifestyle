using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.CerealSideDishes)]
    internal sealed class CerealSideDish : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CerealSideDish" /> with macronutrient and maximum amount.
        /// </summary>
        public CerealSideDish() : base(MacroNutrient.Carbohydrate, MealComponentAllowedAmounts.CerealSideDishes)
        {
        }
    }
}