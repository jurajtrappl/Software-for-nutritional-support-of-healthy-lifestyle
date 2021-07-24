using Application.Core.Common.Enums;
using Application.Core.Common.NutritionalParameters;
using Application.Infrastructure.Entities;
using Application.Web.Areas.App.ViewModels.Profile;
using Application.Web.Constants;
using Application.Web.Extensions;
using Application.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace Application.Web.Areas.App.Controllers
{
    /// <summary>
    /// Profile actions.
    /// </summary>
    [Area(AreaNames.App)]
    [Authorize]
    [Log]
    public class ProfileController : ExtendedBaseController
    {
        /// <summary>
        /// Localizer of shared resources.
        /// </summary>
        private readonly IStringLocalizer _sharedLocalizer;

        public ProfileController(
            IStringLocalizer<SharedResources> sharedLocalizer,
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _sharedLocalizer = sharedLocalizer;
        }

        /// <summary>
        /// GET: App/Profile/Index.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();

            var translatedSexTypes = _sharedLocalizer.TranslateEnum<Sex>("SexTypes");
            var translatedApplicationPlans = _sharedLocalizer.TranslateEnum<ApplicationPlan>("ApplicationPlans");

            ProfileViewModel model = new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Height = user.Measurement?.Height.ToString() ?? "-",
                Weight = user.Measurement?.Weight.ToString() ?? "-",
                Sex = user.Profile is null ? "-" : translatedSexTypes[user.Profile!.SexType],
                AppPlan = user.AppPlan is null ? "-" : translatedApplicationPlans[(ApplicationPlan)user.AppPlan!],
                Bmi = user.Measurement is null ? "-" : new BodyMassIndex(user.Measurement!).Value.ToString(),
                Age = user.Profile?.Age.ToString() ?? "-"
            };

            return View(model);
        }
    }
}