using Application.Core.Common.Enums;
using Application.Core.Nutrition.MealComponents;
using Application.Core.Nutrition.MealComponents.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.EatingOccasions.Base
{
    /// <summary>
    /// Describes the meal that is eaten after <see cref="MidMorningSnack" /> and before <see cref="AfternoonSnack" />.
    /// </summary>
    internal abstract class Lunch : MealOccasion
    {
        /// <summary>
        /// Gets combinations of meal components that are used to make <see cref="Lunch" />.
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
        public override Meal Type => Meal.Lunch;

        /// <summary>
        /// Gets the type of the vegetable that accompany <seealso cref="Lunch" />.
        /// </summary>
        public override WeightLimitedMealComponent Vegetable => new MainMealVegetable();
    }
}