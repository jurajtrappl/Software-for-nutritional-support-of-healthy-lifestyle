namespace Application.Core.Common.Enums
{
    /// <summary>
    /// Types of meals in nutrition meal plan.
    /// </summary>
    public enum Meal
    {
        /// <summary>
        /// First meal of the day.
        /// </summary>
        Breakfast,

        /// <summary>
        /// Eaten after breakfast and before lunch.
        /// </summary>
        MidMorningSnack,

        /// <summary>
        /// Eaten between snacks.
        /// </summary>
        Lunch,

        /// <summary>
        /// Eaten after lunch and before dinner.
        /// </summary>
        AfternoonSnack,

        /// <summary>
        /// Eaten in the evening.
        /// </summary>
        Dinner,

        /// <summary>
        /// The last meal of the day.
        /// </summary>
        Supper
    }
}