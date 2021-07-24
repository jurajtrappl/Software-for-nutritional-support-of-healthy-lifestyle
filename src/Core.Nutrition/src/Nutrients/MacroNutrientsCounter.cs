using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComposers.Summaries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Nutrition.Nutrients
{
    /// <summary>
    /// Data wrapper around macronutrients. Provides helper methods for calculation.
    /// </summary>
    internal struct MacroNutrientsCounter
    {
        public Dictionary<MacroNutrient, double> Current { get; }

        /// <summary>
        /// It is allowed to miss the needed amount by -5%.
        /// </summary>
        private const double LowerBound = 0.95;

        /// <summary>
        /// It is allowed to miss the needed amount by +5%.
        /// </summary>
        private const double UpperBound = 1.05;

        /// <summary>
        /// Required amount of macronutrients needed to be in the meal.
        /// </summary>
        private readonly Dictionary<MacroNutrient, double> _required;

        /// <summary>
        /// Initializes a new instance of <see cref="MacroNutrientsCounter" /> with the given required macronutrients amounts.
        /// </summary>
        /// <param name="required">reuired amount of macronutrients in grams.</param>
        public MacroNutrientsCounter(Dictionary<MacroNutrient, double> required)
        {
            var macroNutrientTypes = (MacroNutrient[])Enum.GetValues(typeof(MacroNutrient));
            Current = macroNutrientTypes.ToDictionary(macroNutrient => macroNutrient, _ => 0.0);
            _required = required;
        }

        /// <summary>
        /// Additon of two counters is only about summing up the current values.
        /// </summary>
        /// <param name="first">first to add.</param>
        /// <param name="second">second to add.</param>
        public static MacroNutrientsCounter operator +(MacroNutrientsCounter first, MacroNutrientsCounter second)
        {
            MacroNutrientsCounter merged = first;
            foreach (var (macroNutrient, amount) in second.Current)
            {
                merged.Current[macroNutrient] += amount;
            }
            return merged;
        }

        /// <summary>
        /// Adds amounts of macronutrients for the given amount of the given <see cref="Ingredient" />.
        /// </summary>
        /// <param name="ingredient">The ingredient from which are macro nutrients counted in.</param>
        /// <param name="ingredientAmount">An amount of the ingredient.</param>
        public void CountInIngredient(IIngredient ingredient, double ingredientAmount)
        {
            foreach (MacroNutrient macroNutrient in (MacroNutrient[])Enum.GetValues(typeof(MacroNutrient)))
            {
                Current[macroNutrient] +=
                    ingredient.MacroNutrients[macroNutrient] *
                    (ingredientAmount / ComposingConstants.ReferenceAmount);
            }
        }

        /// <summary>
        /// Returns the difference between current amount and required amount of the given macronutrient type.
        /// </summary>
        /// <param name="macroNutrient">macronutrient type to find the difference.</param>
        public double GetMacroNutrientDifference(MacroNutrient macroNutrient)
            => Math.Abs(_required[macroNutrient] - Current[macroNutrient]);

        /// <summary>
        /// Returns the required amount of the given macronutrient type according to the given ratio.
        /// </summary>
        /// <param name="macroNutrient">macronutrient type of the required amount value.</param>
        /// <param name="ratio">ratio of the required amount.</param>
        public double GetRequiredAmountFor(MacroNutrient macroNutrient, double ratio)
            => ratio * _required[macroNutrient];

        /// <summary>
        /// Indicates whether current amount of the given macronutrient is under the limit.
        /// </summary>
        /// <param name="macroNutrient">macronutrient type to check.</param>
        /// <returns>True if <see cref="Current" /> is in under lower bound; otherwise False.</returns>
        public bool IsMacroNutrientDeficiency(MacroNutrient macroNutrient)
            => Current[macroNutrient] <= _required[macroNutrient] * LowerBound;

        /// <summary>
        /// Indicates whether current amount of the given macronutrient is over the limit.
        /// </summary>
        /// <param name="macroNutrient">macronutrient type to check.</param>
        /// <returns>True if <see cref="Current" /> is exceeded; otherwise False.</returns>
        public bool IsMacroNutrientExcess(MacroNutrient macroNutrient)
            => Current[macroNutrient] >= _required[macroNutrient] * UpperBound;

        /// <summary>
        /// Determine the new amount of macro nutrients for a given <see cref="Ingredient" />.
        /// </summary>
        /// <param name="ingredientSummary">The ingredient for which are macro nutrients recounted.</param>
        /// <param name="previousAmount">The previous amount of the ingredient.</param>
        public void RecountIngredient(
            IngredientSummary ingredientSummary,
            double previousAmount)
        {
            if (ingredientSummary is null)
            {
                throw new ArgumentNullException(nameof(ingredientSummary));
            }

            CountInIngredient(ingredientSummary.Ingredient, -previousAmount);
            CountInIngredient(ingredientSummary.Ingredient, ingredientSummary.Amount);
        }

        /// <summary>
        /// Round amount of macronutrients to 0 decimal points.
        /// </summary>
        public void RoundAmounts()
        {
            foreach (var (macroNutrient, amount) in Current)
            {
                Current[macroNutrient] = amount.ToZeroDecimals();
            }
        }

        /// <summary>
        /// Set the value of macronutrient to 100% of required value.
        /// </summary>
        /// <param name="macroNutrient">type of macronutrient to set.</param>
        public void SetMacroNutrientToRequired(MacroNutrient macroNutrient)
        {
            Current[macroNutrient] = _required[macroNutrient];
        }
    }
}