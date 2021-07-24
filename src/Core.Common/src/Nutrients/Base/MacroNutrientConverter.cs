namespace Application.Core.Common.Nutrients.Base
{
    /// <summary>
    /// Base type for converters between macronutrient in grams and macronutrient in kilojoules.
    /// </summary>
    public abstract class MacroNutrientConverter
    {
        /// <summary>
        /// Converts macronutrients in grams to macronutrient in kilojoules.
        /// </summary>
        /// <param name="macroNutrientGrams">amount to convert.</param>
        /// <returns>macronutrients in kilojoules converted from <paramref name="macroNutrientGrams" />.</returns>
        internal abstract double FromGramsToKj(double macroNutrientGrams);

        /// <summary>
        /// Converts macronutrients in kilojoules to macronutrient in grams.
        /// </summary>
        /// <param name="macroNutrientKj">amout to convert.</param>
        /// <returns>macronutrients in grams converted from <paramref name="macroNutrientKj" />.</returns>
        internal abstract double FromKjToGrams(double macroNutrientKj);
    }
}