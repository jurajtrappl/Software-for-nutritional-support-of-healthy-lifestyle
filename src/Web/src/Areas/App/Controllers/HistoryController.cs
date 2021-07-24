using Application.Infrastructure.Entities;
using Application.Web.Areas.App.ViewModels.History;
using Application.Web.Constants;
using Application.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Web.Areas.App.Controllers
{
    /// <summary>
    /// History actions.
    /// </summary>
    [Area(AreaNames.App)]
    [Authorize]
    [Log]
    public class HistoryController : ExtendedBaseController
    {
        public HistoryController(UserManager<ApplicationUser> userManager) : base(userManager)
        {
        }

        /// <summary>
        /// GET: App/History/Empty.
        /// </summary>
        public IActionResult Empty()
        {
            return View();
        }

        /// <summary>
        /// GET: App/History/Index.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();

            HistoryListViewModel model = new()
            {
                History = user.History
            };

            return (user.History.Count == 0) ?
                RedirectToAction(nameof(Empty)) :
                View(nameof(Index), model);
        }
    }
}