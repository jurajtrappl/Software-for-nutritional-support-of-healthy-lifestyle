namespace Application.Core.Nutrition.Constants
{
    /// <summary>
    /// Defines the proportion of the ingredient in the generation of macronutrient against other ingredient.
    /// </summary>
    internal static class MealComponentRatios
    {
        /// <summary>
        /// Cereal proportion against fruit when generating carbohydrates.
        /// </summary>
        internal const double CerealRatioForFruit = 0.5;

        /// <summary>
        /// Fruit proportion against cereal when generating carbohydrates.
        /// </summary>
        internal const double FruitRatioForCereal = 0.5;
    }
}