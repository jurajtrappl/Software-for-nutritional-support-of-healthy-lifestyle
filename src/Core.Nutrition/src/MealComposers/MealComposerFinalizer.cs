using Application.Core.Common.Enums;
using Application.Core.Nutrition.Ingredients;
using Application.Core.Nutrition.MealComposers.Summaries;
using System;

namespace Application.Core.Nutrition.Nutrients
{
    /// <summary>
    /// Contains logic for several important logical steps of meal composing algorhitm: adding deficits, aligning
    /// exceeding macronutrients and performing clean up.
    /// </summary>
    internal sealed class MealComposerFinalizer
    {
        /// <summary>
        /// Prepared array of macro nutrients.
        /// </summary>
        private static readonly MacroNutrient[] _macroNutrientTypes =
            (MacroNutrient[])Enum.GetValues(typeof(MacroNutrient));

        /// <summary>
        /// Summary of meal scheduling process.
        /// </summary>
        private MealComponentsSummary _mealSummary;

        /// <summary>
        /// Initializes a new instance of <see cref="MealComposerFinalizer" /> with the given meal component summary.
        /// </summary>
        /// <param name="mealSummary">summary of meal scheduling process.</param>
        public MealComposerFinalizer(MealComponentsSummary mealSummary)
        {
            _mealSummary = mealSummary;
        }

        /// <summary>
        /// Adding clean components to the meal when there is a deficit of any macronutrient.
        /// </summary>
        /// <param name="ingredientPicker">selector of ingredients.</param>
        /// <returns>this instance.</returns>
        internal MealComposerFinalizer AddDeficits(IngredientPicker ingredientPicker)
        {
            foreach (MacroNutrient macroNutrient in _macroNutrientTypes)
            {
                if (_mealSummary.MacroNutrients.IsMacroNutrientDeficiency(macroNutrient))
                {
                    _mealSummary.AddCleanComponent(macroNutrient, ingredientPicker);
                }
            }

            return this;
        }

        /// <summary>
        /// Decreasing amount of ingredients when there is a macronutrient excess.
        /// </summary>
        /// <returns>this instance.</returns>
        internal MealComposerFinalizer AlignExceeding()
        {
            foreach (MacroNutrient macroNutrient in _macroNutrientTypes)
            {
                if (_mealSummary.MacroNutrients.IsMacroNutrientExcess(macroNutrient))
                {
                    _mealSummary.AlignExceedingMacroNutrient(macroNutrient);
                }
            }

            return this;
        }

        /// <summary>
        /// Performing finishing touches on meal summary (rounding, deleting ingredients with less than minimum amount).
        /// </summary>
        /// <returns>modified meal summary.</returns>
        internal MealComponentsSummary CleanUp()
        {
            _mealSummary.RoundIngredientsAmounts();
            _mealSummary.MacroNutrients.RoundAmounts();
            _mealSummary.DeleteUnweightedIngredients();
            return _mealSummary;
        }
    }
}