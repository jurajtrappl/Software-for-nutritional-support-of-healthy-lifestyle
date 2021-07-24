using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Nutrition.EatingOccasions.Base;
using Application.Core.Nutrition.MealComposers.Base;
using Application.Core.Nutrition.MealComposers.Summaries;
using Application.Core.Nutrition.Nutrients;
using System;
using System.Collections.Generic;

namespace Application.Core.Nutrition.MealComposers
{
    /// <summary>
    /// Creates a meal from pattern and adds vegetable.
    /// </summary>
    internal sealed class MealComposer : MealComposerBase
    {
        /// <summary>
        /// Map of meal types and needed macronutrient amount for them.
        /// </summary>
        private readonly Dictionary<Meal, Dictionary<MacroNutrient, double>> _requiredMacroNutrientAmounts;

        /// <summary>
        /// Initializes a new instance of <see cref="MealComposer" /> with the given ingredient collections and required
        /// macronutrient amounts.
        /// </summary>
        /// <param name="ingredients">available ingredients pool.</param>
        /// <param name="requiredMacroNutrientAmounts">required macronutrient amounts for all meal types.</param>
        public MealComposer(
            IReadOnlyDictionary<string, List<IIngredient>> ingredients,
            Dictionary<Meal, Dictionary<MacroNutrient, double>> requiredMacroNutrientAmounts) : base(ingredients)
        {
            _requiredMacroNutrientAmounts = requiredMacroNutrientAmounts ??
                throw new ArgumentNullException(nameof(requiredMacroNutrientAmounts));
        }

        /// <summary>
        /// Creates a food from one of the pattern defined by the given meal type. It consists of main components and
        /// vegetable component. In addition, it satisfies macronutrients needs.
        /// </summary>
        /// <param name="meal">meal occasion type to compose.</param>
        internal override MealComponentsSummary Compose(MealOccasion meal)
        {
            MealComponentsSummary mealSummary = PrepareMainComponents(meal) + SelectVegetable(meal);

            ComputeMacronutrients(mealSummary);

            return new MealComposerFinalizer(mealSummary)
                .AlignExceeding()
                .AddDeficits(IngredientPicker)
                .CleanUp();
        }

        /// <summary>
        /// Calculates ingredient amounts that satisfies macronutrient needs.
        /// </summary>
        /// <param name="macroNutrient">macronutrient that is met.</param>
        /// <param name="mealSummary">summary of the meal.</param>
        /// <param name="ingredientsSummaries">sequence of ingredients that needs to be calculated.</param>
        private static Dictionary<string, double> CalculateIngredientAmounts(
            MacroNutrient macroNutrient,
            MealComponentsSummary mealSummary,
            IEnumerable<IngredientSummary> ingredientsSummaries)
        {
            Dictionary<string, double> ingredientAmounts = new();
            foreach (IngredientSummary summary in ingredientsSummaries)
            {
                double macroNutrientNeededAmount = mealSummary.MacroNutrients
                    .GetRequiredAmountFor(macroNutrient, summary.Ratio);

                double ingredientAmount = summary.CalculateAmount(macroNutrient, macroNutrientNeededAmount);
                ingredientAmounts.Add(summary.Ingredient.Name, ingredientAmount);
            }
            return ingredientAmounts;
        }

        /// <summary>
        /// Satisfies macronutrient needs.
        /// </summary>
        /// <param name="mealSummary">summary of the meal.</param>
        private void ComputeMacronutrients(MealComponentsSummary mealSummary)
        {
            foreach (MacroNutrient macroNutrient in MacroNutrientTypes)
            {
                var macroNutrientIngredients = mealSummary.GetIngredientsByMacroNutrient(macroNutrient);

                var amounts = CalculateIngredientAmounts(macroNutrient, mealSummary, macroNutrientIngredients);
                foreach (var (ingredientName, amount) in amounts)
                {
                    mealSummary.MacroNutrients
                        .CountInIngredient(mealSummary.Ingredients[ingredientName].Ingredient, amount);

                    mealSummary.UpdateAmount(mealSummary.Ingredients[ingredientName].Ingredient, amount);
                }
            }
        }

        /// <summary>
        /// Selects a random pattern and ingredients for components in the pattern.
        /// </summary>
        /// <param name="meal">meal occasion type.</param>
        /// <returns>summary with initial ingredients within main components.</returns>
        private MealComponentsSummary PrepareMainComponents(MealOccasion meal)
        {
            var randomPattern = meal.MainComponentsPatterns.SelectRandom();
            MealComponentsSummary mealSummary = new(randomPattern, _requiredMacroNutrientAmounts[meal.Type]);
            mealSummary.SelectIngredients(IngredientPicker);
            return mealSummary;
        }

        /// <summary>
        /// Selects vegetable for the prepared meal.
        /// </summary>
        /// <param name="meal">meal for which is the vegetable added.</param>
        /// <returns>meal components summary with selected vegetable.</returns>
        private MealComponentsSummary SelectVegetable(MealOccasion meal)
            => new VegetableComponentComposer(IngredientPicker).Compose(meal);
    }
}