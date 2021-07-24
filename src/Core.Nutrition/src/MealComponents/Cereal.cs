using Application.Core.Common.Enums;
using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.Constants;
using Application.Core.Nutrition.MealComponents.Base;
using System.Collections.Generic;

namespace Application.Core.Nutrition.MealComponents
{
    [CollectionName(MealCollectionNames.Cereals)]
    internal sealed class Cereal : WeightLimitedMealComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Cereal" /> with macronutreint and maximum amount.
        /// </summary>
        public Cereal() : base(MacroNutrient.Carbohydrate, MealComponentAllowedAmounts.Cereals)
        {
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        internal override Dictionary<string, double> SameMacroNutrientRatios =>
            new() { { nameof(Fruit), MealComponentRatios.CerealRatioForFruit } };
    }
}