using Application.Core.Nutrition.MealComponents;
using Application.Core.Nutrition.MealComponents.Base;
using Application.Core.Nutrition.MealComposers.Base;

namespace Application.Core.Nutrition.MealComposers
{
    /// <summary>
    /// Adds a component with ingredient that has only carbohydrates.
    /// </summary>
    internal sealed class CleanCarbohydrateComposer : CleanComponentComposer
    {
        /// <summary>
        /// Gets clean carbohydrate meal component.
        /// </summary>
        protected override MealComponent CleanComponent => new CleanCarbohydrate();
    }
}