using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.TuberSideDishes)]
    internal sealed class TuberSideDish : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TuberSideDish" /> with macronutrient and maximum amount.
        /// </summary>
        public TuberSideDish() : base(MacroNutrient.Carbohydrate, MealComponentAllowedAmounts.TuberSideDishes)
        {
        }
    }
}