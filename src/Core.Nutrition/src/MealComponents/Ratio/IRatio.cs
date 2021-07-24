using Application.Core.Nutrition.MealComponents.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.MealComponents.Ratio
{
    /// <summary>
    /// Contract that supports settings ratios between clashing meal components for the same macronutrient.
    /// </summary>
    internal interface IRatiable
    {
        /// <summary>
        /// Sets ratios for each meal component in the given components list.
        /// </summary>
        /// <param name="components">list of meal components for which are the ratios set.</param>
        void SetRatio(IReadOnlyList<WeightLimitedMealComponent> components);
    }
}