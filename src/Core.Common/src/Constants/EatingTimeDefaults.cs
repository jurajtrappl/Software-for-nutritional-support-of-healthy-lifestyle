namespace Application.Core.Common.Constants
{
    public static class EatingTimeDefaults
    {
        /// <summary>
        /// Default breakfast time.
        /// </summary>
        public const int Breakfast = 6;

        /// <summary>
        /// Default midmorning snack time.
        /// </summary>
        public const int MidMorningSnack = 9;

        /// <summary>
        /// Default lunch time.
        /// </summary>
        public const int Lunch = 12;

        /// <summary>
        /// Default afternoon snack time.
        /// </summary>
        public const int AfternoonSnack = 15;

        /// <summary>
        /// Default dinner time.
        /// </summary>
        public const int Dinner = 18;

        /// <summary>
        /// Default supper time.
        /// </summary>
        public const int Supper = 21;

        /// <summary>
        /// Minimum time that should be between two meals.
        /// </summary>
        public const int MinimumIntervalBetweenMeals = 90;

        /// <summary>
        /// Recommended time for eating.
        /// </summary>
        public const int EatingDuration = 30;
    }
}