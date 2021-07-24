using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders an image.
    /// </summary>
    public sealed class ImageViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string path, string alt, string @class = "")
        {
            ImageViewModel model = new(path, alt, @class);
            return View(model);
        }
    }
}