using Application.Core.Common.Constants;
using Application.Core.Common.Enums;
using Application.Core.Common.NutritionalParameters;
using Application.Core.Interfaces;
using Application.Infrastructure.Entities;
using Application.Web.Areas.Main.Models.Home;
using Application.Web.Constants;
using Application.Web.Extensions;
using Application.Web.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Application.Web.Areas.Main.Controllers
{
    /// <summary>
    /// The main home page actions.
    /// </summary>
    [Area(AreaNames.Main)]
    [Log]
    public sealed class HomeController : Controller
    {
        /// <summary>
        /// Localizer of controller resources.
        /// </summary>
        private readonly IStringLocalizer _controllerLocalizer;

        /// <summary>
        /// Web application logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Localizer of shared resources.
        /// </summary>
        private readonly IStringLocalizer _sharedLocalizer;

        public HomeController(
            IStringLocalizer<HomeController> controllerLocalizer,
            ILogger<HomeController> logger,
            IStringLocalizer<SharedResources> sharedLocalizer)
        {
            (_controllerLocalizer, _logger, _sharedLocalizer) =
                (controllerLocalizer, logger, sharedLocalizer);
        }

        /// <summary>
        /// GET: Main/Home/Index.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: Main/Home/Plans.
        /// </summary>
        public IActionResult Plans()
        {
            return View();
        }

        /// <summary>
        /// GET: Main/Home/Functions.
        /// </summary>
        public IActionResult Functions()
        {
            return View();
        }

        /// <summary>
        /// GET: Main/Home/BmiCalculator.
        /// </summary>
        public IActionResult BmiCalculator()
        {
            BmiModel model = new() { IsFrequentlyExercising = true };
            return View(model);
        }

        /// <summary>
        /// GET: Main/Home/ChoosePlanInAdvance.
        /// </summary>
        public IActionResult ChoosePlanInAdvance([FromQuery] string planName)
        {
            if (string.IsNullOrEmpty(planName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(planName)));
            }

            CookieOptions option = new()
            {
                Expires = DateTime.Now.AddHours(CookiesConstants.ChosenPlanInAdvanceDuration),
                Secure = true,
                HttpOnly = true,
                IsEssential = true
            };
            Response.Cookies.Append(CookiesConstants.ChosenPlanInAdvanceName, planName, option);

            _logger.LogDebug(string.Format(LoggerMessages.AddedToCookies, "AppPlan", planName));
            return RedirectToAction(
                nameof(Account.Controllers.HomeController.Register),
                ControllerNames.Home,
                new { area = AreaNames.Account });
        }

        /// <summary>
        /// POST: Main/Home/FindSuitablePlans.
        /// </summary>
        public IActionResult FindSuitablePlans(
            [FromForm] BmiModel model,
            [FromServices] IApplicationPlansSuitabilityService suitabilityService)
        {
            Measurement measurement = new() { Weight = model.Weight!.Value, Height = model.Height!.Value };
            var suitablePlans = suitabilityService.FindSuitablePlans(measurement, model.IsFrequentlyExercising);

            _logger.LogInformation(
                string.Format(
                    LoggerMessages.SuitablePlansFound,
                    model.Weight,
                    model.Height,
                    model.IsFrequentlyExercising,
                    string.Join(',', suitablePlans.Select(p => p.ToString()))));
            var translatedPlans = _sharedLocalizer.TranslateEnum<ApplicationPlan>("ApplicationPlans");
            return Json(
                new
                {
                    suitablePlansHeading = _controllerLocalizer["RecommendedPlans"].Value,
                    suitablePlans = suitablePlans.Select(p => translatedPlans[p]).ToList(),
                    bmi = new BodyMassIndex(measurement).Value
                });
        }
    }
}