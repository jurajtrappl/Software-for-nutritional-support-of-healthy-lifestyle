using Application.Core.Common.Enums;
using Application.Core.Nutrition.MealComponents;
using Application.Core.Nutrition.MealComponents.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.EatingOccasions.Base
{
    /// <summary>
    /// Describes the meal that is eaten as the first meal of the day.
    /// </summary>
    internal abstract class Breakfast : MealOccasion
    {
        /// <summary>
        /// Gets combinations of meal components that are used to make <see cref="Breakfast" />.
        /// </summary>
        public override IEnumerable<List<WeightLimitedMealComponent>> MainComponentsPatterns =>
            new List<List<WeightLimitedMealComponent>>
            {
                new() { new BreakfastEggProtein(), new Cereal(), new Fruit(), new Nut() },
                new() { new BreakfastMeatProtein(), new Cereal(), new Fruit(), new Nut() },
                new() { new BreakfastMilkProtein(), new Cereal(), new Fruit(), new Nut() }
            };

        /// <summary>
        /// Gets the type of the meal.
        /// </summary>
        public override Meal Type => Meal.Breakfast;

        /// <summary>
        /// Gets the type of the vegetable that accompany <seealso cref="Breakfast" />.
        /// </summary>
        public override WeightLimitedMealComponent Vegetable => new SecondaryMealVegetable();
    }
}