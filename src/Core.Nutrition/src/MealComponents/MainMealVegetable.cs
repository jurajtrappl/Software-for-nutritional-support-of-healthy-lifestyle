using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.MainMealVegetables)]
    [NonModifiableComponent]
    internal sealed class MainMealVegetable : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MainMealVegetable" /> with macronutrient and maximum amount.
        /// </summary>
        public MainMealVegetable() : base(MacroNutrient.Carbohydrate, MealComponentAllowedAmounts.Vegetables)
        {
        }
    }
}