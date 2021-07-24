using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders temporary view message.
    /// </summary>
    public sealed class TempDataMessageViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string statusColor, string message)
        {
            TempDataMessageViewModel model = new(message, statusColor);
            return View(model);
        }
    }
}