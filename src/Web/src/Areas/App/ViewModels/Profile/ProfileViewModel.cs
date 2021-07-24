namespace Application.Web.Areas.App.ViewModels.Profile
{
    /// <summary>
    /// View model to display profile information.
    /// </summary>
    public sealed class ProfileViewModel
    {
        /// <summary>
        /// Gets or initializes users first name.
        /// </summary>
        public string FirstName { get; init; }

        /// <summary>
        /// Gets or initializes users last name.
        /// </summary>
        public string LastName { get; init; }

        /// <summary>
        /// Gets or initializes users height.
        /// </summary>
        public string Height { get; init; }

        /// <summary>
        /// Gets or initializes users weight.
        /// </summary>
        public string Weight { get; init; }

        /// <summary>
        /// Gets or initializes users sex typ.
        /// </summary>
        public string Sex { get; init; }

        /// <summary>
        /// Gets or initializes application plan.
        /// </summary>
        public string AppPlan { get; init; }

        /// <summary>
        /// Gets or initializes users age.
        /// </summary>
        public string Age { get; init; }

        /// <summary>
        /// Gets or initializes users body mass index.
        /// </summary>
        public string Bmi { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="ProfileViewModel"/>.
        /// </summary>
        public ProfileViewModel()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Height = string.Empty;
            Weight = string.Empty;
            Sex = string.Empty;
            AppPlan = string.Empty;
            Age = string.Empty;
            Bmi = string.Empty;
        }
    }
}
