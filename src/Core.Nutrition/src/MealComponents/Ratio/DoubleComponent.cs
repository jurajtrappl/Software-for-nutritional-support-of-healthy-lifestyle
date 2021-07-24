using Application.Core.Nutrition.MealComponents.Base;
using System;
using System.Collections.Generic;

namespace Application.Core.Nutrition.MealComponents.Ratio
{
    /// <summary>
    /// Strategy for setting ratios when two meal components clash in generating macronutrient.
    /// </summary>
    internal sealed class DoubleComponent : IRatiable
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="components"><inheritdoc /></param>
        public void SetRatio(IReadOnlyList<WeightLimitedMealComponent> components)
        {
            if (components is null)
            {
                throw new ArgumentNullException(nameof(components));
            }

            string firstName = components[0].GetType().Name;
            string secondName = components[1].GetType().Name;

            components[0].Ratio = components[0].SameMacroNutrientRatios[secondName];
            components[1].Ratio = components[1].SameMacroNutrientRatios[firstName];
        }
    }
}