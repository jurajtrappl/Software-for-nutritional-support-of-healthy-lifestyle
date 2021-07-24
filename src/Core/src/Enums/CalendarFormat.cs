namespace Application.Core.Enums
{
    /// <summary>
    /// Types of supported calendar exporting formats.
    /// </summary>
    public enum CalendarFormat
    {
        /// <summary>
        /// CSV file with specified columns compatible with Google calendar.
        /// </summary>
        Csv,

        /// <summary>
        /// ics, ical, ifb, icalendar file formats.
        /// </summary>
        Ical
    }
}