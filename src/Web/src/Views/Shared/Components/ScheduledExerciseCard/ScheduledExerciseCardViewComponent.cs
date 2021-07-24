using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders card with exercise content on homepage.
    /// </summary>
    public sealed class ScheduledExerciseCardViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int duration, string duringDay, string translatedSportName)
        {
            ScheduledExerciseCardViewModel model = new(duration, duringDay, translatedSportName);
            return View(model);
        }
    }
}