using Application.Core.Common.Enums;
using Application.Core.Nutrition.MealComponents;
using Application.Core.Nutrition.MealComponents.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.EatingOccasions.Base
{
    /// <summary>
    /// Describes the meal that is eaten after <see cref="AfternoonSnack" /> and before <see cref="Supper" />.
    /// </summary>
    internal abstract class Dinner : MealOccasion
    {
        /// <summary>
        /// Gets combinations of meal components that are used to make <see cref="Dinner" />.
        /// </summary>
        public override IEnumerable<List<WeightLimitedMealComponent>> MainComponentsPatterns =>
            new List<List<WeightLimitedMealComponent>>
            {
                new() { new LinnerProtein(), new Oil(), new CerealSideDish() },
                new() { new LinnerProtein(), new Oil(), new PastaSideDish() },
                new() { new LinnerProtein(), new Oil(), new TuberSideDish() }
            };

        /// <summary>
        /// Gets the type of the meal.
        /// </summary>
        public override Meal Type => Meal.Dinner;

        /// <summary>
        /// Gets the type of the vegetable that accompany <seealso cref="Dinner" />.
        /// </summary>
        public override WeightLimitedMealComponent Vegetable => new MainMealVegetable();
    }
}