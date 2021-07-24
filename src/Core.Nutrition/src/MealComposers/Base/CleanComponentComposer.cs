using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.Extensions;
using Application.Core.Nutrition.Ingredients;
using Application.Core.Nutrition.MealComponents.Base;
using System;

namespace Application.Core.Nutrition.MealComposers.Base
{
    /// <summary>
    /// Base class for composer that adds meal component with single macronutrient ingredient.
    /// </summary>
    internal abstract class CleanComponentComposer
    {
        /// <summary>
        /// Type of single macronutrient component.
        /// </summary>
        protected abstract MealComponent CleanComponent { get; }

        /// <summary>
        /// Prepares additional ingredient and its amount to satisfy the given needed amount of given macronutrient.
        /// </summary>
        /// <param name="macroNutrient">macronutrient in deficit.</param>
        /// <param name="neededMacroNutrientAmount">deficit in grams.</param>
        /// <param name="ingredientPicker">available ingredients.</param>
        /// <returns>ingredient and its amount.</returns>
        internal (IIngredient, double) GetCleanIngredient(
            MacroNutrient macroNutrient,
            double neededMacroNutrientAmount,
            IngredientPicker ingredientPicker)
        {
            if (ingredientPicker is null)
            {
                throw new ArgumentNullException(nameof(ingredientPicker));
            }

            Ingredient ingredient = ingredientPicker.SelectIngredient(CleanComponent.GetCollectionName());

            double ingredientAmount =
                neededMacroNutrientAmount /
                ingredient.MacroNutrients[macroNutrient] *
                ComposingConstants.ReferenceAmount;

            return (ingredient, ingredientAmount);
        }
    }
}