using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.Extensions;
using Application.Core.Nutrition.Ingredients;
using Application.Core.Nutrition.MealComponents.Base;
using Application.Core.Nutrition.MealComponents.Ratio;
using Application.Core.Nutrition.Nutrients;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Nutrition.MealComposers.Summaries
{
    /// <summary>
    /// Data type that encapsulates essential information about meal component during calculation.
    /// </summary>
    internal struct MealComponentsSummary
    {
        /// <summary>
        /// Gets components.
        /// </summary>
        internal IReadOnlyList<WeightLimitedMealComponent> Components { get; }

        /// <summary>
        /// Gets or sets map of names of ingredients and their summaries.
        /// </summary>
        internal Dictionary<string, IngredientSummary> Ingredients { get; set; }

        /// <summary>
        /// Gets or sets counter for macronutrients.
        /// </summary>
        internal MacroNutrientsCounter MacroNutrients { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="MealComponentsSummary" /> with the given components and amount of
        /// needed macronutrients.
        /// </summary>
        /// <param name="components">list of components in the summary.</param>
        /// <param name="neededAmounts">amount of macronutrients that a meal being composed must satisfy.</param>
        internal MealComponentsSummary(
            IReadOnlyList<WeightLimitedMealComponent> components,
            Dictionary<MacroNutrient, double> neededAmounts)
        {
            Components = components ?? throw new ArgumentNullException(nameof(components));
            Ingredients = new();
            MacroNutrients = new(neededAmounts);
        }

        /// <summary>
        /// Addition of two summaries unifies ingredients and sums up their macronutrients.
        /// </summary>
        /// <param name="first">the first summary to add.</param>
        /// <param name="second">the second summary to add.</param>
        /// <returns>Summed up <see cref="MealComponentsSummary" /></returns>
        public static MealComponentsSummary operator +(MealComponentsSummary first, MealComponentsSummary second)
        {
            MealComponentsSummary merged = first;
            foreach (var (name, ingredientSummary) in second.Ingredients)
            {
                merged.Ingredients.Add(name, ingredientSummary);
            }
            merged.MacroNutrients += second.MacroNutrients;

            return merged;
        }

        /// <summary>
        /// Adds clean component to the summary for the given macronutrient that is in the deficit.
        /// </summary>
        /// <param name="macroNutrient">macronutrient in deficit.</param>
        /// <param name="ingredientPicker">ingredients selector.</param>
        internal void AddCleanComponent(MacroNutrient macroNutrient, IngredientPicker ingredientPicker)
        {
            double deficit = MacroNutrients.GetMacroNutrientDifference(macroNutrient);

            var (ingredient, ingredientAmount) =
                CleanComponentComposerContext.Compose(macroNutrient, deficit, ingredientPicker);

            if (!Ingredients.ContainsKey(ingredient.Name))
            {
                Ingredients.Add(ingredient.Name, new(ingredientAmount, ingredient, macroNutrient));
            }

            MacroNutrients.SetMacroNutrientToRequired(macroNutrient);
        }

        /// <summary>
        /// Reduce the amount of excess macronutrients to 100% of needed amount.
        /// </summary>
        /// <param name="exceedingMacroNutrient">macronutrient type to align.</param>
        internal void AlignExceedingMacroNutrient(MacroNutrient exceedingMacroNutrient)
        {
            var previousAmounts = SnapshotCurrentAmounts(exceedingMacroNutrient);
            AlignIngredientAmounts(exceedingMacroNutrient);
            RecountMacroNutrients(previousAmounts);
        }

        /// <summary>
        /// Deletes every ingredient that has less amount than minimum possible.
        /// </summary>
        internal void DeleteUnweightedIngredients()
        {
            var weightedComponents =
                (from i in Ingredients
                 where i.Value.Amount >= MealComponentAllowedAmounts.Minimum
                 select new { name = i.Key, summary = i.Value })
                 .ToDictionary(key => key.name, value => value.summary);

            Ingredients = weightedComponents;
        }

        /// <summary>
        /// Generates a sequence of ingredients in the summary according to the most represented macronutrient. In
        /// addition, the ingredients must be modifiable.
        /// </summary>
        /// <param name="macroNutrient">the most represented macronutrient.</param>
        /// <param name="isModifiable">boolean value if its modifiable.</param>
        /// <returns>Seuqence of ingredients summary that satisfy the condition.</returns>
        internal IEnumerable<IngredientSummary> GetIngredientsByMacroNutrient(
            MacroNutrient macroNutrient,
            bool isModifiable = false)
        {
            return from i in Ingredients.Values
                   where i.Modifiable != isModifiable
                   where i.Majoritarian == macroNutrient
                   select i;
        }

        /// <summary>
        /// Round amount of ingredients to zero decimal places.
        /// </summary>
        internal void RoundIngredientsAmounts()
        {
            foreach (var (name, ingredientSummary) in Ingredients)
            {
                double previousAmount = ingredientSummary.Amount;
                Ingredients[name] = ingredientSummary with { Amount = previousAmount.ToZeroDecimals() };
            }
        }

        /// <summary>
        /// For each meal component selects an appropriate ingredient using the given <see cref="IngredientPicker" />
        /// and initializes <see cref="IngredientSummary" />.
        /// </summary>
        /// <param name="ingredientPicker">ingredients selector.</param>
        internal void SelectIngredients(IngredientPicker ingredientPicker)
        {
            Dictionary<WeightLimitedMealComponent, string> components = InitIngredientComponents(ingredientPicker);
            InitIngredientSummaries(components);
        }

        /// <summary>
        /// Increments given amount of the given ingredient.
        /// </summary>
        /// <param name="ingredient">ingredient to update.</param>
        /// <param name="addAmount">additional amount.</param>
        internal void UpdateAmount(IIngredient ingredient, double addAmount)
        {
            if (ingredient is null)
            {
                throw new ArgumentNullException(nameof(ingredient));
            }

            if (Ingredients.ContainsKey(ingredient.Name))
            {
                double previousAmount = Ingredients[ingredient.Name].Amount;
                Ingredients[ingredient.Name] = Ingredients[ingredient.Name] with { Amount = previousAmount + addAmount };
            }
        }

        /// <summary>
        /// After completion of this method, each ingredient that generates the given macronutrient will be aligned so
        /// that macronutrient amount will be satisfied up to max 100%.
        /// </summary>
        /// <param name="macroNutrient"></param>
        private void AlignIngredientAmounts(MacroNutrient macroNutrient)
        {
            double excess = MacroNutrients.GetMacroNutrientDifference(macroNutrient);
            foreach (var ingredient in GetIngredientsByMacroNutrient(macroNutrient))
            {
                double macroNutrientDecrease = excess * ingredient.Ratio;

                UpdateAmount(
                    ingredient.Ingredient,
                    -(macroNutrientDecrease /
                    ingredient.Ingredient.MacroNutrients[macroNutrient] *
                    ComposingConstants.ReferenceAmount));
            }
        }

        /// <summary>
        /// Adds ingredient to each of present meal components in <see cref="Components" /> and stores the result for
        /// the next initialization process.
        /// </summary>
        /// <param name="ingredientPicker">ingredients selector.</param>
        /// <returns></returns>
        private Dictionary<WeightLimitedMealComponent, string> InitIngredientComponents(IngredientPicker ingredientPicker)
        {
            if (ingredientPicker is null)
            {
                throw new ArgumentNullException(nameof(ingredientPicker));
            }

            Dictionary<WeightLimitedMealComponent, string> ingredientComponents = new();
            foreach (var component in Components)
            {
                Ingredient ingredient = ingredientPicker.SelectIngredient(component.GetCollectionName());

                Ingredients.Add(ingredient.Name, new(ingredient, component));
                ingredientComponents.Add(component, ingredient.Name);
            }
            return ingredientComponents;
        }

        /// <summary>
        /// For each given <see cref="WeightLimitedMealComponent" /> component initiliazes a helper <see
        /// cref="IngredientSummary" /> structure.
        /// </summary>
        /// <param name="components">components for summaries.</param>
        internal void InitIngredientSummaries(Dictionary<WeightLimitedMealComponent, string> components)
        {
            foreach (MacroNutrient macroNutrient in (MacroNutrient[])Enum.GetValues(typeof(MacroNutrient)))
            {
                var comps = Components.ToList().FindAll(c => c.Majoritarian == macroNutrient);
                if (comps.Count > 0)
                {
                    RatioContext.SetRatios(comps);

                    foreach (WeightLimitedMealComponent component in comps)
                    {
                        string ingredientName = components[component];

                        IngredientSummary ingredient = Ingredients[ingredientName];
                        ingredient.Ratio = component.Ratio;

                        Ingredients[ingredientName] = ingredient;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the amount of macronutrients.
        /// </summary>
        /// <param name="previousAmounts">previous amount of ingredients.</param>
        private void RecountMacroNutrients(Dictionary<string, double> previousAmounts)
        {
            if (previousAmounts is null)
            {
                throw new ArgumentNullException(nameof(previousAmounts));
            }

            foreach (var (ingredientName, previousAmount) in previousAmounts)
            {
                MacroNutrients.RecountIngredient(Ingredients[ingredientName], previousAmount);
            }
        }

        /// <summary>
        /// Creates a map of ingredient names and their amounts for the given macronutrient.
        /// </summary>
        /// <param name="macroNutrient">macronutrient that is generated by ingredients.</param>
        private Dictionary<string, double> SnapshotCurrentAmounts(MacroNutrient macroNutrient)
            => GetIngredientsByMacroNutrient(macroNutrient)
                .ToDictionary(key => key.Ingredient.Name, value => value.Amount);
    }
}