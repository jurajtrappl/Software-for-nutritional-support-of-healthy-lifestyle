using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.Extensions;
using Application.Core.Nutrition.MealComponents.Base;
using System;

namespace Application.Core.Nutrition.MealComposers.Summaries
{
    /// <summary>
    /// Data type that encapsulates essential information about ingredient during calculation.
    /// </summary>
    internal record IngredientSummary
    {
        /// <summary>
        /// Gets or sets amount.
        /// Unit: grams.
        /// </summary>
        internal double Amount { get; set; }

        /// <summary>
        /// Gets or sets ingredient.
        /// </summary>
        internal IIngredient Ingredient { get; set; }

        /// <summary>
        /// Gets or sets maximum amount of ingredient.
        /// Unit: grams.
        /// </summary>
        internal int MaximumAmount { get; set; }

        /// <summary>
        /// Gets or sets modifiability of ingredient amount.
        /// </summary>
        internal bool Modifiable { get; set; }

        /// <summary>
        /// Gets or set the most represent macronutrient.
        /// </summary>
        internal MacroNutrient Majoritarian { get; set; }

        /// <summary>
        /// Gets or sets ratio.
        /// </summary>
        internal double Ratio { get; set; }

        public IngredientSummary(
            double amount,
            IIngredient ingredient,
            MacroNutrient majoritarian)
        {
            (Amount, Ingredient, Majoritarian) = (amount, ingredient, majoritarian);
        }

        public IngredientSummary(IIngredient ingredient, WeightLimitedMealComponent component, double amount = 0, double ratio = 0) :
            this(amount, ingredient, component.Majoritarian)
        {
            if (component is null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            Ingredient = ingredient ?? throw new ArgumentNullException(nameof(ingredient));
            (MaximumAmount, Modifiable, Ratio) = (component.MaximumAmount, component.CanBeModified(), ratio);
        }

        /// <summary>
        /// Calculates the amount of <see cref="Ingredient" /> needed to satisfy the given macronutrient need.
        /// </summary>
        /// <param name="macroNutrient">macronutrient that needs to be added.</param>
        /// <param name="macroNutrientNeeded">needed amount of macronutrient in grams.</param>
        /// <returns>amount of ingredients in grams.</returns>
        internal double CalculateAmount(MacroNutrient macroNutrient, double macroNutrientNeeded)
        {
            double amount =
                (macroNutrientNeeded
                / Ingredient.MacroNutrients[macroNutrient]
                * ComposingConstants.ReferenceAmount).ToTwoDecimals();
            return (amount > MaximumAmount) ? MaximumAmount : amount;
        }
    }
}