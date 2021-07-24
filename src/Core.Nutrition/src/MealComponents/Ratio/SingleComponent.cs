using Application.Core.Nutrition.MealComponents.Base;
using System;
using System.Collections.Generic;

namespace Application.Core.Nutrition.MealComponents.Ratio
{
    /// <summary>
    /// Strategy for setting ratios when singe component generates macronutrient.
    /// </summary>
    internal sealed class SingleComponent : IRatiable
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

            components[0].Ratio = 1;
        }
    }
}