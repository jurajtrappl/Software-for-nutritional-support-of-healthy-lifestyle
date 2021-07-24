using Application.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders description of application plan.
    /// </summary>
    public sealed class PlanDescriptionViewComponent : ViewComponent
    {
        /// <summary>
        /// Localizer of shared resources.
        /// </summary>
        private readonly IHtmlLocalizer _localizer;

        /// <summary>
        /// Initialize a new instance of <see cref="PlanDescriptionViewComponent"/>
        /// with the given shared resources localizer.
        /// </summary>
        /// <param name="localizer">localizer of shared resources.</param>
        public PlanDescriptionViewComponent(IHtmlLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
        }

        public IViewComponentResult Invoke(string name, string[] colNames)
        {
            if (colNames is null)
            {
                throw new ArgumentNullException(nameof(colNames));
            }

            int screenRatio = (colNames.Length == 0) ? 0 : BootstrapDefaults.NumberOfColumns / colNames.Length;
            PlanDescriptionViewModel model = new(name, _localizer, screenRatio, colNames);
            return View(model);
        }
    }
}