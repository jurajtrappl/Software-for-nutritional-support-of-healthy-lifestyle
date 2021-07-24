namespace Application.Web.Areas.App.Models.Calendar
{
    /// <summary>
    /// Model for calendar index view that contains export calendar form.
    /// </summary>
    public sealed class ExportCalendarModel
    {
        /// <summary>
        /// Gets or sets calendar format.
        /// </summary>
        public string Format { get; set; } = string.Empty;
    }
}