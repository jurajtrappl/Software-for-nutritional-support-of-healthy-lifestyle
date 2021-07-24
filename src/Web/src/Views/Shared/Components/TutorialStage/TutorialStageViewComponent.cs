using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders one tutorial stage on application home page.
    /// </summary>
    public sealed class TutorialStageViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(
            IViewLocalizer localizer,
            string name,
            int orderNum,
            string imgPath,
            string imgAlt)
        {
            TutorialStageViewModel model = new(name, localizer, orderNum, imgPath, imgAlt);
            return View(model);
        }
    }
}