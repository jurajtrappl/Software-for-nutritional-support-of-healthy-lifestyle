using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.CleanProteins)]
    internal sealed class CleanProtein : MealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CleanProtein" /> with macronutrient.
        /// </summary>
        public CleanProtein() : base(MacroNutrient.Protein)
        {
        }
    }
}