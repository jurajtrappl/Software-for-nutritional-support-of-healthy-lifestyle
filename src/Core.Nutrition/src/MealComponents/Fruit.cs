using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.Fruits)]
    internal sealed class Fruit : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Fruit" /> with macronutrient and maximum amount.
        /// </summary>
        public Fruit() : base(MacroNutrient.Carbohydrate, MealComponentAllowedAmounts.Fruits)
        {
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        internal override Dictionary<string, double> SameMacroNutrientRatios =>
            new() { { nameof(Cereal), MealComponentRatios.FruitRatioForCereal } };
    }
}