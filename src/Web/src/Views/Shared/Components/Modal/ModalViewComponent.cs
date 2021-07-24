using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders bootstrap modal.
    /// </summary>
    public sealed class ModalViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string id, string labelId, string bodyId, string @class)
        {
            ModalViewModel model = new(id, labelId, bodyId, @class);
            return View(model);
        }
    }
}