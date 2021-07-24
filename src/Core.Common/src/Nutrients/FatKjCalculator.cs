using Application.Core.Common.Nutrients.Base;
using Application.Core.Nutrition.Constants;

namespace Application.Core.Common.Nutrients
{
    /// <summary>
    /// Fat gram/kj and kj/gram converter.
    /// </summary>
    internal sealed class FatKjCalculator : MacroNutrientConverter
    {
        /// <summary>
        /// Converts fats in grams to carbohydrates in kilojoules.
        /// </summary>
        /// <param name="macroNutrientGrams"><inheritdoc /></param>
        /// <returns>fats in kilojoules converted from <paramref name="macroNutrientGrams" />.</returns>
        internal override double FromGramsToKj(double macroNutrientGrams)
            => macroNutrientGrams * MacroNutrientToKjConstants.KjFatToGrams;

        /// <summary>
        /// Converts fats in kilojoules to fats in grams.
        /// </summary>
        /// <param name="macroNutrientKj"><inheritdoc /></param>
        /// <returns>fats in grams converted from <paramref name="macroNutrientKj" />.</returns>
        internal override double FromKjToGrams(double macroNutrientKj)
            => macroNutrientKj / MacroNutrientToKjConstants.KjFatToGrams;
    }
}