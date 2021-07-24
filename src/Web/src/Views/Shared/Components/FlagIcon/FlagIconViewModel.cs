namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for flag icon component.
    /// </summary>
    public sealed class FlagIconViewModel
    {
        /// <summary>
        /// Gets or initializes css icon class.
        /// </summary>
        public string IconClass { get; init; }

        /// <summary>
        /// Gets or initializes current culture name.
        /// </summary>
        public string CurrentCultureName { get; init; }

        /// <summary>
        /// Gets or initializes callback url.
        /// </summary>
        public string ReturnUrl { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="FlagIconViewModel"/>
        /// with the given icon class, current culture name and return url.
        /// </summary>
        /// <param name="iconClass">css icon class.</param>
        /// <param name="currentCultureName">current culture name.</param>
        /// <param name="returnUrl">callback url.</param>
        public FlagIconViewModel(string iconClass, string currentCultureName, string returnUrl)
        {
            (IconClass, CurrentCultureName, ReturnUrl) = (iconClass, currentCultureName, returnUrl);
        }
    }
}