using Application.Core.Nutrition.MealComponents.Base;
using System;
using System.Collections.Generic;

namespace Application.Core.Nutrition.MealComponents.Ratio
{
    /// <summary>
    /// Context for selecting ratio strategies.
    /// </summary>
    internal sealed class RatioContext
    {
        /// <summary>
        /// Map of strategies, keys are number of components.
        /// </summary>
        private static readonly Dictionary<int, Func<IRatiable>> _strategyMap =
            new()
            {
                { 1, () => new SingleComponent() },
                { 2, () => new DoubleComponent() }
            };

        /// <summary>
        /// Sets ratios for each meal component in the given components list.
        /// </summary>
        /// <param name="components">list of meal components for which are the ratios set.</param>
        public static void SetRatios(IReadOnlyList<WeightLimitedMealComponent> components)
        {
            if (components is null)
            {
                throw new ArgumentNullException(nameof(components));
            }

            _strategyMap[components.Count]().SetRatio(components);
        }
    }
}