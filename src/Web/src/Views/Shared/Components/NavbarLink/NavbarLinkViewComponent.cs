using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component renders a navigation bar link.
    /// </summary>
    public sealed class NavbarLinkViewComponent : ViewComponent
    {
        /// <summary>
        /// Localizer of shared resources.
        /// </summary>
        private readonly IStringLocalizer _sharedLocalizer;

        /// <summary>
        /// Initializes a new instance of <see cref="NavbarLinkViewComponent"/> 
        /// with the given shared localizer.
        /// </summary>
        /// <param name="sharedLocalizer">localizer of shared resources.</param>
        public NavbarLinkViewComponent(IStringLocalizer<SharedResources> sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
        }

        public IViewComponentResult Invoke(string name, object actionHtmlAttributes)
        {
            NavbarLinkViewModel model = new(actionHtmlAttributes, _sharedLocalizer, name);
            return View(model);
        }
    }
}