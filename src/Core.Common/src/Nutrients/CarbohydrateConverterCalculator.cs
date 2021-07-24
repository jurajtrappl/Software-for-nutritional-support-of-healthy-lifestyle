using Application.Core.Common.Nutrients.Base;
using Application.Core.Nutrition.Constants;

namespace Application.Core.Common.Nutrients
{
    /// <summary>
    /// Carbohydrate gram/kj and kj/gram converter.
    /// </summary>
    internal sealed class CarbohydrateConverterCalculator : MacroNutrientConverter
    {
        /// <summary>
        /// Converts carbohydrates in grams to carbohydrates in kilojoules.
        /// </summary>
        /// <param name="macroNutrientGrams"><inheritdoc /></param>
        /// <returns>carbohydrates in kilojoules converted from <paramref name="macroNutrientGrams" />.</returns>
        internal override double FromGramsToKj(double macroNutrientGrams)
            => macroNutrientGrams * MacroNutrientToKjConstants.KjCarbohydrateToGrams;

        /// <summary>
        /// Converts carbohydrates in kilojoules to carbohydrates in grams.
        /// </summary>
        /// <param name="macroNutrientKj"><inheritdoc /></param>
        /// <returns>carbohydrates in grams converted from <paramref name="macroNutrientKj" />.</returns>
        internal override double FromKjToGrams(double macroNutrientKj)
            => macroNutrientKj / MacroNutrientToKjConstants.KjCarbohydrateToGrams;
    }
}