using Application.Core.Nutrition.MealComponents;
using Application.Core.Nutrition.MealComponents.Base;
using Application.Core.Nutrition.MealComposers.Base;

namespace Application.Core.Nutrition.MealComposers
{
    /// <summary>
    /// Adds a component with ingredient that has only fat.
    /// </summary>
    internal sealed class CleanFatComposer : CleanComponentComposer
    {
        /// <summary>
        /// Gets clean fat meal component.
        /// </summary>
        protected override MealComponent CleanComponent => new CleanFat();
    }
}