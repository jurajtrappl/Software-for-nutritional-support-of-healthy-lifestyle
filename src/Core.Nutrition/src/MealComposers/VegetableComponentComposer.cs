using Application.Core.Common.Enums;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.EatingOccasions.Base;
using Application.Core.Nutrition.Ingredients;
using Application.Core.Nutrition.MealComponents.Base;
using Application.Core.Nutrition.MealComposers.Base;
using Application.Core.Nutrition.MealComposers.Summaries;
using System.Collections.Generic;

namespace Application.Core.Nutrition.MealComposers
{
    /// <summary>
    /// Adds compulsory vegetable portion to each meal.
    /// </summary>
    internal sealed class VegetableComponentComposer : MealComposerBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="VegetableComponentComposer" /> with the given ingredient picker.
        /// </summary>
        /// <param name="ingredientPicker">selector of ingredient.</param>
        public VegetableComponentComposer(IngredientPicker ingredientPicker) : base(ingredientPicker)
        {
        }

        /// <summary>
        /// Composes vegetable component for the given meal.
        /// </summary>
        /// <param name="meal">meal for which is the vegetable being composed.</param>
        /// <returns>meal summary with vegetable.</returns>
        internal override MealComponentsSummary Compose(MealOccasion meal)
        {
            MealComponentsSummary vegetableSummary =
                new(new List<WeightLimitedMealComponent>() { meal.Vegetable }, new());

            vegetableSummary.SelectIngredients(IngredientPicker);

            AddVegetableAmount(vegetableSummary);

            return vegetableSummary;
        }

        /// <summary>
        /// Calculate vegetable and macronutrients amount and count them in to the given summary.
        /// </summary>
        /// <param name="vegetableSummary">meal summary for which is the vegetable being counted in.</param>
        private static void AddVegetableAmount(MealComponentsSummary vegetableSummary)
        {
            var vegetables = vegetableSummary
                .GetIngredientsByMacroNutrient(MacroNutrient.Carbohydrate, isModifiable: true);
            foreach (var vegetable in vegetables)
            {
                vegetableSummary.MacroNutrients.CountInIngredient(
                    vegetable.Ingredient,
                    MealComponentAllowedAmounts.Vegetables);
                vegetableSummary.UpdateAmount(
                    vegetable.Ingredient,
                    MealComponentAllowedAmounts.Vegetables);
            }
        }
    }
}