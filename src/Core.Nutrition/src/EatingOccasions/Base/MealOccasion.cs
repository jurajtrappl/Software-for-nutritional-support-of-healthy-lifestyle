using Application.Core.Common.Enums;
using Application.Core.Nutrition.MealComponents.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.EatingOccasions.Base
{
    /// <summary>
    /// Base class for eating occasions.
    /// </summary>
    internal abstract class MealOccasion
    {
        /// <summary>
        /// Gets ratio of the meal in whole day energetic intake based on the specified meal frequency.
        /// </summary>
        public abstract double TeeRatio { get; }

        /// <summary>
        /// Gets combinations of main meal components that are used to make food.
        /// </summary>
        public abstract IEnumerable<IReadOnlyList<WeightLimitedMealComponent>> MainComponentsPatterns { get; }

        /// <summary>
        /// Gets the type of the meal.
        /// </summary>
        public abstract Meal Type { get; }

        /// <summary>
        /// Gets the type of the vegetable that accompany a dish.
        /// </summary>
        public abstract WeightLimitedMealComponent Vegetable { get; }
    }
}