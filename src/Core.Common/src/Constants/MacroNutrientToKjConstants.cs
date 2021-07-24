namespace Application.Core.Nutrition.Constants
{
    /// <summary>
    /// Conversion rates between 1kJ of macronutrient and grams.
    /// </summary>
    public static class MacroNutrientToKjConstants
    {
        /// <summary>
        /// 1 g of carbohydrate is 17 kJ.
        /// </summary>
        public const double KjCarbohydrateToGrams = 17;

        /// <summary>
        /// 1 g of fat is 38 kJ.
        /// </summary>
        public const double KjFatToGrams = 38;

        /// <summary>
        /// 1 g of protein is 17 kJ.
        /// </summary>
        public const double KjProteinToGrams = 17;
    }
}