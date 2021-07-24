using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Nutrition.MealComposers.Summaries;
using System.Collections.Generic;

namespace Application.Core.Nutrition.Schedulers
{
    /// <summary>
    /// Represents a schedueled meal plan item.
    /// </summary>
    public sealed class ScheduledMeal : IScheduledMeal
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ScheduledMeal" /> with the given meal type and summary.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="summary"></param>
        internal ScheduledMeal(Meal type, MealComponentsSummary summary)
        {
            Ingredients = new();
            MacroNutrients = summary.MacroNutrients.Current;
            Type = type;

            InitIngredients(summary);
        }

        /// <summary>
        /// Processes meal components summary and passes data to data properties defined by the IScheduledMeal contract.
        /// </summary>
        /// <param name="summary">summary after composing a meal.</param>
        private void InitIngredients(MealComponentsSummary summary)
        {
            foreach (var (name, ingredientSummary) in summary.Ingredients)
            {
                Ingredients.Add(name, ingredientSummary.Amount);
            }
        }

        /// <summary>
        /// Gets or sets ingredients present in the food.
        /// </summary>
        public Dictionary<string, double> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets macronutrients contained in the food.
        /// </summary>
        public Dictionary<MacroNutrient, double> MacroNutrients { get; set; }

        /// <summary>
        /// Gets or sets meal type.
        /// </summary>
        public Meal Type { get; set; }
    }
}