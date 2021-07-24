namespace Application.Web.Areas.App.Models.Settings
{
    /// <summary>
    /// Model for change settings forms.
    /// </summary>
    public sealed class ChangeSettingsModel
    {
        /// <summary>
        /// Gets or initializes changed password model.
        /// </summary>
        public ChangePasswordModel ChangePassword { get; init; } = new();

        /// <summary>
        /// Gets or initializes profile information model.
        /// </summary>
        public ChangeProfileInformationModel ChangeProfileInformation { get; init; } = new();

        /// <summary>
        /// Gets or iniatializes plan model.
        /// </summary>
        public SelectedPlanModel ChangePlan { get; init; } = new();

        /// <summary>
        /// Gets or sets eating time configuration model.
        /// </summary>
        public ChangeEatingTimeConfigModel ChangeEatingTimeConfig { get; init; } = new();
    }
}