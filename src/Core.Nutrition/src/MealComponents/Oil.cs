using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.Oils)]
    internal sealed class Oil : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Oil" /> with macronutrient and maximum amount.
        /// </summary>
        public Oil() : base(MacroNutrient.Fat, MealComponentAllowedAmounts.Oils)
        {
        }
    }
}