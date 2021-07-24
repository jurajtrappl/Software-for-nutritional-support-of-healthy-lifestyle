using Application.Core.Nutrition.MealComponents;
using Application.Core.Nutrition.MealComponents.Base;
using Application.Core.Nutrition.MealComposers.Base;

namespace Application.Core.Nutrition.MealComposers
{
    /// <summary>
    /// Adds a component with ingredient that has only protein.
    /// </summary>
    internal sealed class CleanProteinComposer : CleanComponentComposer
    {
        /// <summary>
        /// Gets clean protein meal component.
        /// </summary>
        protected override MealComponent CleanComponent => new CleanProtein();
    }
}