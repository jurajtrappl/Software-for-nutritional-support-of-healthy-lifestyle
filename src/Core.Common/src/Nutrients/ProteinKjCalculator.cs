using Application.Core.Common.Nutrients.Base;
using Application.Core.Nutrition.Constants;

namespace Application.Core.Common.Nutrients
{
    /// <summary>
    /// Protein gram/kj and kj/gram converter.
    /// </summary>
    internal sealed class ProteinKjCalculator : MacroNutrientConverter
    {
        /// <summary>
        /// Converts proteins in grams to proteins in kilojoules.
        /// </summary>
        /// <param name="macroNutrientGrams"><inheritdoc /></param>
        /// <returns>carbohydrates in kilojoules converted from <paramref name="macroNutrientGrams" /></returns>
        internal override double FromGramsToKj(double macroNutrientGrams)
            => macroNutrientGrams * MacroNutrientToKjConstants.KjProteinToGrams;

        /// <summary>
        /// Converts proteins in kilojoules to proteins in grams.
        /// </summary>
        /// <param name="macroNutrientKj"><inheritdoc /></param>
        /// <returns>proteins in grams converted from <paramref name="macroNutrientKj" /></returns>
        internal override double FromKjToGrams(double macroNutrientKj)
            => macroNutrientKj / MacroNutrientToKjConstants.KjProteinToGrams;
    }
}