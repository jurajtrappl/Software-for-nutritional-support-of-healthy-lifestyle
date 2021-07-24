using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Nutrition.EatingOccasions.Base;
using Application.Core.Nutrition.Ingredients;
using Application.Core.Nutrition.MealComposers.Summaries;
using System;
using System.Collections.Generic;

namespace Application.Core.Nutrition.MealComposers.Base
{
    /// <summary>
    /// Base class for different types of meal composers.
    /// </summary>
    internal abstract class MealComposerBase
    {
        /// <summary>
        /// Appropriate ingredient selector.
        /// </summary>
        protected readonly IngredientPicker IngredientPicker;

        /// <summary>
        /// Collection of macronutrients.
        /// </summary>
        protected readonly MacroNutrient[] MacroNutrientTypes =
            (MacroNutrient[])Enum.GetValues(typeof(MacroNutrient));

        protected MealComposerBase(IngredientPicker ingredientPicker)
        {
            IngredientPicker = ingredientPicker ?? throw new ArgumentNullException(nameof(ingredientPicker));
        }

        protected MealComposerBase(IReadOnlyDictionary<string, List<IIngredient>> ingredients)
        {
            IngredientPicker = new(ingredients);
        }

        /// <summary>
        /// Creates meal from one of the pattern given by the given meal.
        /// </summary>
        /// <param name="meal">meal occasion to create.</param>
        /// <returns><see cref="MealComponentsSummary" /> of the created meal.</returns>
        internal abstract MealComponentsSummary Compose(MealOccasion meal);
    }
}