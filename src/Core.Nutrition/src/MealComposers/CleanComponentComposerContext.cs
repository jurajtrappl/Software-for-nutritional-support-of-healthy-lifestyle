using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Nutrition.Ingredients;
using Application.Core.Nutrition.MealComposers.Base;
using System;
using System.Collections.Generic;

namespace Application.Core.Nutrition.MealComposers
{
    /// <summary>
    /// Provides contextual information when composing ingredients with single macronutrient.
    /// </summary>
    internal sealed class CleanComponentComposerContext
    {
        /// <summary>
        /// Map of macronutrients and composers.
        /// </summary>
        private static readonly IReadOnlyDictionary<MacroNutrient, Func<CleanComponentComposer>> _composers;

        /// <summary>
        /// Map between macronutrient types and single macronutrient composers for them.
        /// </summary>
        static CleanComponentComposerContext()
        {
            _composers = new Dictionary<MacroNutrient, Func<CleanComponentComposer>>
            {
                { MacroNutrient.Carbohydrate, () => new CleanCarbohydrateComposer() },
                { MacroNutrient.Fat, () => new CleanFatComposer() },
                { MacroNutrient.Protein, () => new CleanProteinComposer() }
            };
        }

        /// <summary>
        /// Prepares additional ingredient and its amount to satisfy the given needed amount of given macronutrient.
        /// </summary>
        /// <param name="macroNutrient">macronutrient in deficit.</param>
        /// <param name="neededMacroNutrientAmount">deficit in grams.</param>
        /// <param name="ingredientPicker">available ingredients.</param>
        /// <returns>ingredient and its amount.</returns>
        internal static (IIngredient, double) Compose(
            MacroNutrient macroNutrient,
            double neededMacroNutrientAmount,
            IngredientPicker ingredientPicker)
        {
            return _composers[macroNutrient]()
                .GetCleanIngredient(macroNutrient, neededMacroNutrientAmount, ingredientPicker);
        }
    }
}