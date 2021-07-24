using Microsoft.Extensions.Localization;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for the navbar link component.
    /// </summary>
    public sealed class NavbarLinkViewModel
    {
        /// <summary>
        /// Gets or initializes action html attributes.
        /// </summary>
        public object ActionHtmlAttributes { get; init; }

        /// <summary>
        /// Gets or initializes localizer.
        /// </summary>
        public IStringLocalizer Localizer { get; init; }

        /// <summary>
        /// Gets or initializes link name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="NavbarLinkViewModel"/> with the given
        /// action html attributes, localizer and name.
        /// </summary>
        /// <param name="actionHtmlAttributes">link action html attributes.</param>
        /// <param name="localizer">resource localizer.</param>
        /// <param name="name">link name.</param>
        public NavbarLinkViewModel(object actionHtmlAttributes, IStringLocalizer localizer, string name)
        {
            (ActionHtmlAttributes, Localizer, Name) = (actionHtmlAttributes, localizer, name);
        }
    }
}