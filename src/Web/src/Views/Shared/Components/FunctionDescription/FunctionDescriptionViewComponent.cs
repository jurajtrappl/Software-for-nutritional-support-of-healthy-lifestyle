using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders a application function description.
    /// </summary>
    public sealed class FunctionDescriptionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IViewLocalizer localizer, string iconName, string colName)
        {
            FunctionDescriptionViewModel model = new(localizer, iconName, colName);
            return View(model);
        }
    }
}