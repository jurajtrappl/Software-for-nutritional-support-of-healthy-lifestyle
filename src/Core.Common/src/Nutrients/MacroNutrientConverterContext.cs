using Application.Core.Common.Enums;
using Application.Core.Common.Nutrients.Base;
using System;
using System.Collections.Generic;

namespace Application.Core.Common.Nutrients
{
    /// <summary>
    /// Contextual information about macronutrient converting.
    /// </summary>
    public static class MacroNutrientConverterContext
    {
        /// <summary>
        /// Map of macronutrient types and converters for each of them.
        /// </summary>
        private static readonly Dictionary<MacroNutrient, Func<MacroNutrientConverter>> _calculators
            = new()
            {
                { MacroNutrient.Carbohydrate, () => new CarbohydrateConverterCalculator() },
                { MacroNutrient.Fat, () => new FatKjCalculator() },
                { MacroNutrient.Protein, () => new ProteinKjCalculator() }
            };

        /// <summary>
        /// Converts grams to kilojoules of the given macronutrient and given amount of it.
        /// </summary>
        /// <param name="macroNutrient">macronutrient type to convert.</param>
        /// <param name="macroNutrientGrams">grams to convert.</param>
        /// <returns>macronutrient in kilojoules converted from <paramref name="macroNutrientGrams" />.</returns>
        public static double FromGramsToKj(MacroNutrient macroNutrient, double macroNutrientGrams)
            => _calculators[macroNutrient]().FromGramsToKj(macroNutrientGrams);

        /// <summary>
        /// Converts kilojoules to grams of the given macronutrient and given amount of it.
        /// </summary>
        /// <param name="macroNutrient">macronutrient type to convert.</param>
        /// <param name="macroNutrientKj">kilojoules to convert.</param>
        /// <returns>macronutrient in grams converted from <paramref name="macroNutrientKj" />.</returns>
        public static double FromKjToGrams(MacroNutrient macroNutrient, double macroNutrientKj)
            => _calculators[macroNutrient]().FromKjToGrams(macroNutrientKj);
    }
}