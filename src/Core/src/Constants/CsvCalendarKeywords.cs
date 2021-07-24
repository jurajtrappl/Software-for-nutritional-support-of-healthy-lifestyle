namespace Application.Core.Constants
{
    /// <summary>
    /// Keywords for formatting calendar to csv format.
    /// </summary>
    internal static class CsvCalendarKeywords
    {
        /// <summary>
        /// Csv file suffix.
        /// </summary>
        internal const string FileFormat = "csv";

        /// <summary>
        /// Column name of the description.
        /// </summary>
        internal const string HeaderDescriptionColumn = "Description";

        /// <summary>
        /// Column name of the end date.
        /// </summary>
        internal const string HeaderEndDateColumn = "End Date";

        /// <summary>
        /// Column name of the end time.
        /// </summary>
        internal const string HeaderEndTimeColumn = "End Time";

        /// <summary>
        /// Column name of the start date.
        /// </summary>
        internal const string HeaderStartDateColumn = "Start Date";

        /// <summary>
        /// Column name of the subject.
        /// </summary>
        internal const string HeaderSubjectColumn = "Subject";

        /// <summary>
        /// Column name of the time start.
        /// </summary>
        internal const string HeaderTimeStartColumn = "Start Time";

        /// <summary>
        /// Name of the MIME type.
        /// </summary>
        internal const string MimeType = "text/csv";
    }
}