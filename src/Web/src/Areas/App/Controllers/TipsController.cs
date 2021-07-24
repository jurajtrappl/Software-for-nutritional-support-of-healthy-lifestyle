using Application.Infrastructure.Entities;
using Application.Web.Constants;
using Application.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Areas.App.Controllers
{
    /// <summary>
    /// Nutrition tips actions.
    /// </summary>
    [Area(AreaNames.App)]
    [Authorize]
    [Log]
    public class TipsController : ExtendedBaseController
    {
        public TipsController(UserManager<ApplicationUser> userManager) : base(userManager)
        {
        }

        /// <summary>
        /// GET: App/Tips/Index.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}