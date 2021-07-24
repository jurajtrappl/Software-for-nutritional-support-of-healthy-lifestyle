namespace Application.Web.Areas.App.Models.Settings
{
    /// <summary>
    /// Model for settings change application plan form.
    /// </summary>
    public sealed class SelectedPlanModel
    {
        /// <summary>
        /// Gets or initializes application plan.
        /// </summary>
        public string ApplicationPlanType { get; init; } = string.Empty;
    }
}