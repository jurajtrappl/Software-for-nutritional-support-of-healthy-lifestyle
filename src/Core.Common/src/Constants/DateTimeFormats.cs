namespace Application.Core.Common.Constants
{
    /// <summary>
    /// Represents the standard date and time format specifiers.
    /// </summary>
    internal static class DateTimeFormats
    {
        /// <summary>
        /// General date/time pattern (short time).
        /// </summary>
        internal const string GeneralDateShortTime = "g";

        /// <summary>
        /// Short date pattern.
        /// </summary>
        internal const string ShortDate = "d";

        /// <summary>
        /// Short time pattern.
        /// </summary>
        internal const string ShortTime = "t";

        /// <summary>
        /// ICalendar format date pattern.
        /// </summary>
        internal const string ICalDate = "yyyyMMdd";

        /// <summary>
        /// ICalendar format time pattern.
        /// </summary>
        internal const string ICalTime = "yyyyMMddTHHmm00";
    }
}