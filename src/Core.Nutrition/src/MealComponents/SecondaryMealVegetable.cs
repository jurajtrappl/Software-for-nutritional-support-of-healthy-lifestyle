using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.SecondaryMealVegetables)]
    [NonModifiableComponent]
    internal sealed class SecondaryMealVegetable : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SecondaryMealVegetable" /> with macronutrient and maximum amount.
        /// </summary>
        public SecondaryMealVegetable() :
            base(MacroNutrient.Carbohydrate, MealComponentAllowedAmounts.Vegetables)
        {
        }
    }
}