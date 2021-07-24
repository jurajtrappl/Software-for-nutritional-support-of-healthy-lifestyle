using Application.Core.Common.Constants;
using Application.Core.Common.Dto;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Common.NutritionalParameters;
using Application.Core.Interfaces;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Extensions;
using Application.Web.Areas.App.Models.Home;
using Application.Web.Constants;
using Application.Web.Extensions;
using Application.Web.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Web.Areas.App.Controllers
{
    /// <summary>
    /// User account home page actions.
    /// </summary>
    [Area(AreaNames.App)]
    [Authorize]
    [Log]
    public class HomeController : ExtendedBaseController
    {
        /// <summary>
        /// Indicates whether has user chosen suitable plan.
        /// </summary>
        private readonly IApplicationPlansSuitabilityService _applicationPlansService;

        /// <summary>
        /// Localizer of controller resources.
        /// </summary>
        private readonly IStringLocalizer _controllerLocalizer;

        /// <summary>
        /// Web application logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Automapper between two objects.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Scheduling service for application plans.
        /// </summary>
        private readonly IPlansSchedulingService _plansSchedulingService;

        /// <summary>
        /// Localizer of shared resources.
        /// </summary>
        private readonly IStringLocalizer _sharedLocalizer;

        public HomeController(
            IApplicationPlansSuitabilityService applicationPlansService,
            IStringLocalizer<HomeController> controllerLocalizer,
            ILogger<HomeController> logger,
            IMapper mapper,
            IPlansSchedulingService plansSchedulingService,
            IStringLocalizer<SharedResources> sharedLocalizer,
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            (_applicationPlansService, _controllerLocalizer, _logger, _mapper, _plansSchedulingService, _sharedLocalizer) =
                (applicationPlansService, controllerLocalizer, logger, mapper, plansSchedulingService, sharedLocalizer);
        }

        /// <summary>
        /// GET: App/Home/Index.
        /// </summary>
        public async Task<IActionResult> Index([FromServices] IScheduledItemsDisplayService planItemizer)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            // current plan ended, but not evaluated yet
            if (user.HasNotEvaluatedPlan())
            {
                return RedirectToAction(
                    nameof(EvaluatePlan),
                    ControllerNames.Home,
                    new { area = AreaNames.App });
            }

            // not on plan right now (either completely new user or with already evaluated last plan and ready for new)
            if (user.IsNotOnPlan())
            {
                // did not choose it from the main page via cookies
                if (!user.HasSelectedApplicationPlan())
                {
                    TempData.Add(TempDataMessages.NotSelectedPlan, _controllerLocalizer["NotSelectedPlan"].Value);
                    return RedirectToAction(
                        nameof(SelectPlan),
                        ControllerNames.Home,
                        new { area = AreaNames.App });
                }

                // has chosen via cookies
                var translatedPlans = _sharedLocalizer.TranslateEnum<ApplicationPlan>("ApplicationPlans");
                TempData.Add(TempDataMessages.ChosenPlan, translatedPlans[(ApplicationPlan)user.AppPlan!]);
                return RedirectToAction(
                    nameof(EnterProfile),
                    ControllerNames.Home,
                    new { area = AreaNames.App });
            }

            // is on plan, but starting tomorrow
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            if (!user.HasAnyPlanOnDate(today) && user.HasAnyPlanOnDate(tomorrow))
            {
                return RedirectToAction(
                    nameof(PlanStartsTomorrow),
                    ControllerNames.Home,
                    new { area = AreaNames.App });
            }

            // is on plan today
            planItemizer.SelectScheduledItemsForDate(user, DateTime.Today, ViewBag);
            return View();
        }

        /// <summary>
        /// GET: App/Home/PlanStartsTomorrow.
        /// </summary>
        public IActionResult PlanStartsTomorrow()
        {
            return View();
        }

        /// <summary>
        /// Displays meal as ingredients and macronutrient list on tables in the Index view.
        /// </summary>
        public async Task<IActionResult> ShowMeal(
            [FromQuery] string date,
            [FromServices] IStringLocalizer<Ingredients> ingredientsLocalizer)
        {
            if (DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateKey))
            {
                ApplicationUser user = await GetCurrentUserAsync();
                IScheduledMeal? scheduledMeal = user.Meals.GetItemOnDate(d => d.IsTheSameDateTimeAs(dateKey));
                if (scheduledMeal is null)
                {
                    _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, date));
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { });
                }

                return Json(
                    new
                    {
                        scheduledMeal = scheduledMeal.TranslateIngredients(ingredientsLocalizer),
                        translatedMacroNutrients = _sharedLocalizer.TranslateEnum<MacroNutrient>("MacroNutrients")
                    });
            }

            _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, date));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { });
        }

        /// <summary>
        /// GET: App/Home/SelectPlan.
        /// </summary>
        public IActionResult SelectPlan()
        {
            return View();
        }

        /// <summary>
        /// GET: App/Home/ChosenPlan.
        /// </summary>
        public async Task<IActionResult> ChosenPlan([FromQuery] string planName)
        {
            if (string.IsNullOrEmpty(planName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(planName)));
            }

            if (Enum.TryParse(planName, out ApplicationPlan applicationPlan))
            {
                var user = await GetCurrentUserAsync();
                user.AppPlan = applicationPlan;

                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    AddErrors(result);
                    return View();
                }

                _logger.LogDebug(string.Format(LoggerMessages.AssignedPlanFromCookies, applicationPlan.ToString(), user.UserName));

                var translatedPlans = _sharedLocalizer.TranslateEnum<ApplicationPlan>("ApplicationPlans");
                if (translatedPlans.TryGetValue(applicationPlan, out string? translatedName))
                {
                    TempData.Add(TempDataMessages.ChosenPlan, translatedName);
                    return RedirectToAction(
                        nameof(EnterProfile),
                        ControllerNames.Home,
                        new { area = AreaNames.App });
                }

                _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInResources, applicationPlan.ToString()));
                return View(nameof(Index));
            }

            _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, planName));
            return View(nameof(Index));
        }

        /// <summary>
        /// GET: App/Home/EnterProfile.
        /// </summary>
        [HttpGet]
        public IActionResult EnterProfile()
        {
            PrepareTranslations();
            return View();
        }

        /// <summary>
        /// POST: App/Home/EnterProfile.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> EnterProfile(UserProfileModel model)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(EnterProfile), user.UserName));

            if (!ModelState.IsValid)
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidModelState, nameof(EnterProfile)));
                PrepareTranslations();
                return View(model);
            }

            var (measurement, profile) = ProcessInputData(model);
            if (!IsSuitablePlanChosen(measurement, model.PhysicalActivityLevel, user))
            {
                _logger.LogDebug(string.Format(LoggerMessages.NotSuitablePlanChosen, user.UserName));
                TempData.Add(TempDataMessages.NotSuitablePlanChosen, _controllerLocalizer["NotSuitablePlanChosen"].Value);
                return RedirectToAction(
                    nameof(SelectPlan),
                    ControllerNames.Home,
                    new { area = AreaNames.App });
            }

            SetNewUserInformation(user, model, measurement, profile);
            _logger.LogInformation(string.Format(LoggerMessages.ScheduledPlans, user.UserName));

            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(
                    nameof(SuccessfulPlanEntry),
                    ControllerNames.Home,
                    new { area = AreaNames.App });
            }

            _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(EnterProfile), user.UserName));
            AddErrors(result);
            PrepareTranslations();
            return View();
        }

        /// <summary>
        /// GET: App/Home/EvaluatePlan.
        /// </summary>
        [HttpGet]
        public IActionResult EvaluatePlan()
        {
            return View();
        }

        /// <summary>
        /// POST: App/Home/EvaluatePlan.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> EvaluatePlan(PlanEvaluationModel model)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(EvaluatePlan), user.UserName));

            if (!ModelState.IsValid || model.Age < user.Profile!.Age)
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidModelState, nameof(EvaluatePlan)));
                ModelState.AddModelError(string.Empty, _controllerLocalizer["AgeCanNotBeLess"].Value);
                return View(model);
            }

            BodyMassIndex currentBmi = new(
                new Measurement
                {
                    Weight = model.Weight,
                    Height = user.Measurement!.Height
                });

            PlanResult planResult = new()
            {
                AppPlan = (ApplicationPlan)user.AppPlan!,
                Start = user.Meals.Keys.First(),
                End = user.Meals.Keys.Last(),
                TotalMealsCount = user.Meals.TotalMealsCount(),
                PreviousBmi = new BodyMassIndex(user.Measurement).Value,
                CurrentBmi = currentBmi.Value,
                TotalKjEaten = user.Meals.TotalKilojouleEaten(),
                TotalMinTrained = user.Exercises is null ? 0 : user.Exercises!.TotalExercisedTime()
            };

            ViewBag.PlanResult = planResult;

            user.History.Add(planResult);
            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(EvaluatePlan), user.UserName));
                return View(nameof(PlanResult));
            }

            _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(EvaluatePlan), user.UserName));
            AddErrors(result);
            return View();
        }

        /// <summary>
        /// GET: App/Home/EndPlan.
        /// </summary>
        public async Task<IActionResult> EndPlan()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(EndPlan), user.UserName));

            user.DeletePlanData();
            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(EndPlan), user.UserName));
                return RedirectToAction(
                    nameof(Index),
                    ControllerNames.Home,
                    new { area = AreaNames.App });
            }

            _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(EndPlan), user.UserName));
            AddErrors(result);
            return RedirectToAction(
                nameof(Index),
                ControllerNames.Home,
                new { area = AreaNames.App });
        }

        /// <summary>
        /// GET: App/Home/SuccessfulPlanEntry.
        /// </summary>
        public IActionResult SuccessfulPlanEntry()
        {
            return View();
        }

        /// <summary>
        /// Initialize the given user with the new data from registration process.
        /// </summary>
        /// <param name="user">user for whom the data are being assigned.</param>
        /// <param name="model">additional user data.</param>
        /// <param name="measurement">users measurement data.</param>
        /// <param name="profile">users profile data.</param>
        private void SetNewUserInformation(
            ApplicationUser user,
            UserProfileModel model,
            IMeasurement measurement,
            IProfile profile)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            user.Measurement = measurement;
            user.Profile = profile;
            user.History ??= new List<IPlanResult>();
            user.Allergens = model.Allergens;

            _plansSchedulingService.CreatePlansFor(user);
        }

        /// <summary>
        /// Indicate whether the given measurement and physical activity level satisfies the selected application plan.
        /// </summary>
        /// <param name="measurement">measurement of the user.</param>
        /// <param name="physicalActivityLevel">physical activity level of the user.</param>
        /// <param name="user">user for whom is being determined the suitability.</param>
        /// <returns>True if <paramref name="user"/> is suitable for the chosen plan; otherwise False.</returns>
        private bool IsSuitablePlanChosen(IMeasurement measurement, string physicalActivityLevel, ApplicationUser user)
        {
            var pal = _sharedLocalizer.GetTranslatedValue<Core.Common.Enums.PhysicalActivityLevel>(
                    "ActivityDifficulties", physicalActivityLevel);
            bool isFrequentlyExercising = pal != Core.Common.Enums.PhysicalActivityLevel.Sedentary;
            return _applicationPlansService.IsMeetingRequirements(
                (ApplicationPlan)user.AppPlan!,
                measurement,
                isFrequentlyExercising);
        }

        /// <summary>
        /// Process <see cref="UserProfileModel"/> in to the two entities, profile and measurement.
        /// </summary>
        /// <param name="model">model to process.</param>
        /// <returns><see cref="IMeasurement"/> and <see cref="IProfile"/> from the <paramref name="model"/>.</returns>
        private (IMeasurement, IProfile) ProcessInputData(UserProfileModel model)
        {
            Sex sexType = _sharedLocalizer.GetTranslatedValue<Sex>("SexTypes", model.SexType);
            Core.Common.Enums.PhysicalActivityLevel pal =
                _sharedLocalizer.GetTranslatedValue<Core.Common.Enums.PhysicalActivityLevel>(
                    "ActivityDifficulties",
                    model.PhysicalActivityLevel);
            IProfile profile = new Infrastructure.Entities.Profile { Age = model.Age, Pal = pal, SexType = sexType };

            IMeasurement measurement = _mapper.Map<Measurement>(model);

            return (measurement, profile);
        }

        /// <summary>
        /// Add translated resources to the viewbag.
        /// </summary>
        private void PrepareTranslations()
        {
            var translatedSexTypes = _sharedLocalizer.ListOf("SexTypes");
            ViewBag.TranslatedSexTypes =
                translatedSexTypes
                    .Select(type => new SelectListItem { Value = type, Text = type });

            var translatedActivityDifficulties = _sharedLocalizer.ListOf("ActivityDifficulties");
            ViewBag.TranslatedActivityDifficulties =
                translatedActivityDifficulties
                    .Select(difficulty => new SelectListItem { Value = difficulty, Text = difficulty });

            var translatedAllergens = _sharedLocalizer.ListOf("Allergens");
            ViewBag.TranslatedAllergens =
                translatedAllergens
                    .Select((allergen, index) => new SelectListItem { Value = index.ToString(), Text = allergen });
        }
    }
}