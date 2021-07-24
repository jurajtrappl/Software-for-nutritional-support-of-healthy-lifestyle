namespace Application.Core.Constants
{
    /// <summary>
    /// Keywords for formatting calendar to ical format.
    /// </summary>
    internal static class IcalendarKeywords
    {
        /// <summary>
        /// Name of the event beginning.
        /// </summary>
        internal const string EventBegin = "BEGIN:VEVENT";

        /// <summary>
        /// Name of the end of event datetime.
        /// </summary>
        internal const string EventDateTimeEnd = "DTEND";

        /// <summary>
        /// Name of the stamp of event datetime.
        /// </summary>
        internal const string EventDateTimeStamp = "DTSTAMP";

        /// <summary>
        /// Name of the start of event datetime.
        /// </summary>
        internal const string EventDateTimeStart = "DTSTART";

        /// <summary>
        /// Name of the description of event.
        /// </summary>
        internal const string EventDescription = "DESCRIPTION";

        /// <summary>
        /// Name of the end of event.
        /// </summary>
        internal const string EventEnd = "END:VEVENT";

        /// <summary>
        /// Name of the summary of event.
        /// </summary>
        internal const string EventSummary = "SUMMARY";

        /// <summary>
        /// Ical file suffix.
        /// </summary>
        internal const string FileFormat = "ical";

        /// <summary>
        /// Name of the footer.
        /// </summary>
        internal const string Footer = "END:VCALENDAR";

        /// <summary>
        /// Name of the beginning of header.
        /// </summary>
        internal const string HeaderBegin = "BEGIN:VCALENDAR";

        /// <summary>
        /// Name of calendar scale in header.
        /// </summary>
        internal const string HeaderCalcscale = "CALSCALE:GREGORIAN";

        /// <summary>
        /// Name of the method in header.
        /// </summary>
        internal const string HeaderMethod = "METHOD:PUBLISH";

        /// <summary>
        /// Name of the production id.
        /// </summary>
        internal const string HeaderProdId = "PRODID:Application";

        /// <summary>
        /// Version.
        /// </summary>
        internal const string HeaderVersion = "VERSION:2.0";

        /// <summary>
        /// Name of the MIME type.
        /// </summary>
        internal const string MimeType = "text/calendar";
    }
}