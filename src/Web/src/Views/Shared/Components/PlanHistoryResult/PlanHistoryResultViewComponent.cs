using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Infrastructure.Entities;
using Application.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders plan result in users history page.
    /// </summary>
    public sealed class PlanHistoryResultViewComponent : ViewComponent
    {
        /// <summary>
        /// Localizer of shared resources.
        /// </summary>
        private readonly IStringLocalizer _sharedLocalizer;

        /// <summary>
        /// Initialize a new instance of <see cref="PlanHistoryResultViewComponent"/>
        /// with the given shared localizer.
        /// </summary>
        /// <param name="sharedLocalizer">localizer of shared resources.</param>
        public PlanHistoryResultViewComponent(IStringLocalizer<SharedResources> sharedLocalizer)
        {
            _sharedLocalizer = sharedLocalizer;
        }

        public IViewComponentResult Invoke(PlanResult planHistory, IViewLocalizer localizer)
        {
            if (planHistory is null)
            {
                throw new ArgumentNullException(nameof(planHistory));
            }

            PlanHistoryResultViewModel model = new(
                planHistory.Start.ToCurrentCultureDateString(),
                planHistory.End.ToCurrentCultureDateString(),
                planHistory,
                localizer,
                _sharedLocalizer.TranslateEnum<ApplicationPlan>("ApplicationPlans"));
            return View(model);
        }
    }
}