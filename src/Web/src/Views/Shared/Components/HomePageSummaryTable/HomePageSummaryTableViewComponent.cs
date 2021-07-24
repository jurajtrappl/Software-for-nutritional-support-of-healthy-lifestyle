using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders summary table for on users home page.
    /// </summary>
    public sealed class HomePageSummaryTableViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IViewLocalizer localizer, string name)
        {
            HomePageSummaryTableViewModel model = new(localizer, name);
            return View(model);
        }
    }
}