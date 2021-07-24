using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.CleanCarbohydrates)]
    internal sealed class CleanCarbohydrate : MealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CleanCarbohydrate" /> with macronutrient.
        /// </summary>
        public CleanCarbohydrate() : base(MacroNutrient.Carbohydrate)
        {
        }
    }
}