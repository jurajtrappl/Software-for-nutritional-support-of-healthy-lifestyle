using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.Nuts)]
    internal sealed class Nut : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Nut" /> with macronutrient and maximum amount.
        /// </summary>
        public Nut() : base(MacroNutrient.Fat, MealComponentAllowedAmounts.Nuts)
        {
        }
    }
}