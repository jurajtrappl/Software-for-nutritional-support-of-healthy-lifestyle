namespace Application.Core.Nutrition.Constants
{
    /// <summary>
    /// Constants limiting the amounts of ingredients generated from above and below.
    /// </summary>
    internal static class MealComponentAllowedAmounts
    {
        /// <summary>
        /// The maximum possible amount of breakfast egg protein ingredient.
        /// </summary>
        internal const int BreakfastEggProteins = 100;

        /// <summary>
        /// The maximum possible amount of breakfast meat protein ingredient.
        /// </summary>
        internal const int BreakfastMeatProteins = 150;

        /// <summary>
        /// The maximum possible amount of breakfast milk protein ingredient.
        /// </summary>
        internal const int BreakfastMilkProteins = 300;

        /// <summary>
        /// The maximum possible amount of cereal ingredient.
        /// </summary>
        internal const int Cereals = 100;

        /// <summary>
        /// The maximum possible amount of fruit ingredient.
        /// </summary>
        internal const int Fruits = 300;

        /// <summary>
        /// The maximum possible amount of nut ingredient.
        /// </summary>
        internal const int Nuts = 70;

        /// <summary>
        /// The maximum possible amount of lunch or dinner ingredient.
        /// </summary>
        internal const int LinnerProteins = 250;

        /// <summary>
        /// The maximum possible amount of oil ingredient.
        /// </summary>
        internal const int Oils = 20;

        /// <summary>
        /// The maximum possible amount of vegetable ingredient.
        /// </summary>
        internal const int Vegetables = 100;

        /// <summary>
        /// The maximum possible amount of cereal side dish ingredient.
        /// </summary>
        internal const int CerealSideDishes = 250;

        /// <summary>
        /// The maximum possible amount of pasta side dish ingredient.
        /// </summary>
        internal const int PastaSideDishes = 250;

        /// <summary>
        /// The maximum possible amount of tuber side dish ingredient.
        /// </summary>
        internal const int TuberSideDishes = 350;

        /// <summary>
        /// The minimum possible amount of any ingredient.
        /// </summary>
        internal const int Minimum = 3;
    }
}