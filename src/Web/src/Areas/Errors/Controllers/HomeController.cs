using Application.Infrastructure.Entities;
using Application.Web.Constants;
using Application.Web.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.Web.Areas.Errors.Controllers
{
    /// <summary>
    /// Errors actions.
    /// </summary>
    [Area(AreaNames.Errors)]
    [Log]
    public class HomeController : ExtendedBaseController
    {
        /// <summary>
        /// Web application logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Identity sing in manager.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(
            ILogger<HomeController> logger,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            (_logger, _signInManager) = (logger, signInManager);
        }

        /// <summary>
        /// GET: App/Errors/ApplicationError.
        /// </summary>
        public async Task<IActionResult> ApplicationError()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            if (user is not null)
            {
                await _signInManager.SignOutAsync();
                _logger.LogDebug(string.Format(LoggerMessages.UserLoggedOut, user.UserName));
            }
            
            return View();
        }
    }
}