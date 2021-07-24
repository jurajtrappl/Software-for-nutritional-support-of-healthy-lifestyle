using Application.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Application.Web
{
    /// <summary>
    /// Provides additional methods for controller working with API for managing users.
    /// </summary>
    public abstract class ExtendedBaseController : Controller
    {
        /// <summary>
        /// API for managing users.
        /// </summary>
        protected readonly UserManager<ApplicationUser> UserManager;

        protected ExtendedBaseController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Gets current user.
        /// </summary>
        /// <returns>Currently logged user.</returns>
        protected async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await UserManager.GetUserAsync(HttpContext.User);
        }

        /// <summary>
        /// Adds errors from <see cref="IdentityResult" /> to the model state.
        /// </summary>
        /// <param name="result">Result errors.</param>
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}