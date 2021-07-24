namespace Application.Core.Common.Constants
{
    /// <summary>
    /// Defines the values for creating calendar UI component and calculating calendar data.
    /// </summary>
    public static class CalendarConstants
    {
        /// <summary>
        /// Number of days per week.
        /// </summary>
        public const int DaysInWeek = 7;

        /// <summary>
        /// Maximum allowed days across months.
        /// </summary>
        public const int MaximumDaysInMonth = 31;

        /// <summary>
        /// The number of the first day of the month.
        /// </summary>
        public const int MonthFirstDay = 1;

        /// <summary>
        /// Number of the first month of the year.
        /// </summary>
        public const int FirstMonth = 1;

        /// <summary>
        /// Number of the last month of the year.
        /// </summary>
        public const int LastMonth = 12;

        /// <summary>
        /// Number of rendered rows in calendar.
        /// </summary>
        public const int WeeksInCalendarComponent = 6;

        /// <summary>
        /// Number of rows in list view of calendar.
        /// </summary>
        public const int ListViewDays = 9;

        /// <summary>
        /// Number of letters when the screen is sm.
        /// </summary>
        public const int WeekDayNameSmallScreenLength = 3;
    }
}