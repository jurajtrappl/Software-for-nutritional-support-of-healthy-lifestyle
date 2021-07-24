using Application.Core.Common.Dto;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Scheduler;
using Application.Core.Dto;
using Application.Core.Interfaces;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Extensions;
using Application.Web.Areas.App.Models.Settings;
using Application.Web.Constants;
using Application.Web.Extensions;
using Application.Web.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Web.Areas.App.Controllers
{
    /// <summary>
    /// Account and plan settings actions.
    /// </summary>
    [Area(AreaNames.App)]
    [Authorize]
    [Log]
    public class SettingsController : ExtendedBaseController
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
        /// Automapper between two objects.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Localizer of shared resources.
        /// </summary>
        private readonly IStringLocalizer _sharedLocalizer;

        public SettingsController(
            IStringLocalizer<SettingsController> controllerLocalizer,
            ILogger<SettingsController> logger,
            IMapper mapper,
            IStringLocalizer<SharedResources> sharedLocalizer,
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            (_controllerLocalizer, _logger, _mapper, _sharedLocalizer) =
                (controllerLocalizer, logger, mapper, sharedLocalizer);
        }

        /// <summary>
        /// GET: App/Settings/Index.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            PrepareDataForIndexView(user);
            return View(FillModel(user));
        }

        /// <summary>
        /// POST: App/Settings/ChangeProfileInformation.
        /// </summary>
        public async Task<IActionResult> ChangeProfileInformation([FromServices] IMailService mailService, ChangeSettingsModel model)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(ChangeProfileInformation), user.UserName));

            if (!IsSubmodelValid(model.ChangeProfileInformation))
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidModelState, nameof(ChangeProfileInformation)));
                PrepareDataForIndexView(user);
                return View(nameof(Index), FillModel(user));
            }

            if (model.ChangeProfileInformation.Username != user.UserName)
            {
                ApplicationUser sameUsernameUser = await UserManager.FindByNameAsync(model.ChangeProfileInformation.Username);
                if (sameUsernameUser is not null)
                {
                    _logger.LogDebug(string.Format(LoggerMessages.AlreadyRegisteredUsername, user.UserName));
                    ModelState.AddModelError(string.Empty, _sharedLocalizer["AlreadyRegisteredUsername"].Value);
                    PrepareDataForIndexView(user);
                    return View(nameof(Index), FillModel(user));
                }
            }

            bool isEmailChanged = false;
            if (model.ChangeProfileInformation.Email != user.Email)
            {
                ApplicationUser sameEmailAddressUser = await UserManager.FindByEmailAsync(model.ChangeProfileInformation.Email);
                if (sameEmailAddressUser is not null)
                {
                    _logger.LogDebug(string.Format(LoggerMessages.AlreadyRegisteredEmail, user.Email));
                    ModelState.AddModelError(string.Empty, _sharedLocalizer["AlreadyRegisteredEmail"].Value);
                    PrepareDataForIndexView(user);
                    return View(nameof(Index), FillModel(user));
                }

                isEmailChanged = true;
            }

            _mapper.Map(model.ChangeProfileInformation, user);

            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (isEmailChanged)
                {
                    await SendChangeProfileInformationEmailConfirmation(mailService, user);
                    _logger.LogInformation(LoggerMessages.EmailHasBeenSent, nameof(ChangeProfileInformation));
                    user.EmailConfirmed = false;

                    result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(ChangeProfileInformation), user.UserName));
                        return RedirectToAction(
                            nameof(Account.Controllers.HomeController.Logout),
                            ControllerNames.Home,
                            new { area = AreaNames.Account });
                    }

                    _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(ChangeProfileInformation), user.UserName));
                    AddErrors(result);
                }

                _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(ChangeProfileInformation), user.UserName));
                TempData.Add(
                    TempDataMessages.ChangeOfProfileInformation,
                    _controllerLocalizer["SuccessfulChangeOfProfileInformation"].Value);
                user = await GetCurrentUserAsync();
            }
            else
            {
                _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(ChangeProfileInformation), user.UserName));
                AddErrors(result);
            }

            PrepareDataForIndexView(user);
            return View(nameof(Index), FillModel(user));
        }

        /// <summary>
        /// POST: App/Home/ChangePassword.
        /// </summary>
        public async Task<IActionResult> ChangePassword(ChangeSettingsModel model)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(ChangePassword), user.UserName));

            if (!IsSubmodelValid(model.ChangePassword))
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidModelState, nameof(ChangeProfileInformation)));
                PrepareDataForIndexView(user);
                return View(nameof(Index), FillModel(user));
            }

            // check current password
            bool isCurrentPassword = await UserManager.CheckPasswordAsync(user, model.ChangePassword.CurrentPassword);
            if (!isCurrentPassword)
            {
                _logger.LogDebug(string.Format(LoggerMessages.WrongPassword, user.UserName));
                ModelState.AddModelError(string.Empty, _controllerLocalizer["WrongPassword"].Value);
                PrepareDataForIndexView(user);
                return View(nameof(Index), FillModel(user));
            }

            var result = await UserManager.ChangePasswordAsync(
                user,
                model.ChangePassword.CurrentPassword,
                model.ChangePassword.NewPassword);

            if (result.Succeeded)
            {
                _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(ChangePassword), user.UserName));
                TempData.Add(TempDataMessages.ChangeOfPassword, _controllerLocalizer["SuccessfulChangeOfPassword"].Value);
                user = await GetCurrentUserAsync();
            }
            else
            {
                _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(ChangePassword), user.UserName));
                AddErrors(result);
            }

            PrepareDataForIndexView(user);
            return View(nameof(Index), FillModel(user));
        }

        /// <summary>
        /// POST: App/Settings/ChangePlan.
        /// </summary>
        public async Task<IActionResult> ChangePlan(
            ChangeSettingsModel model,
            [FromServices] IApplicationPlansSuitabilityService applicationPlansService,
            [FromServices] IPlansSchedulingService plansSchedulingService)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            // newly registered
            if (user.AppPlan is null || user.Meals.Count == 0)
            {
                PrepareDataForIndexView(user);
                return View(nameof(Index), FillModel(user));
            }

            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(ChangePlan), user.UserName));

            // test for the same plan
            ApplicationPlan newAppPlan = _sharedLocalizer.GetTranslatedValue<ApplicationPlan>(
                "ApplicationPlans", model.ChangePlan.ApplicationPlanType);
            if (newAppPlan == user.AppPlan)
            {
                _logger.LogDebug(string.Format(LoggerMessages.SameApplicationPlan, user.UserName));
                ModelState.AddModelError(string.Empty, _controllerLocalizer["AlreadyOnApplicationPlan"].Value);
                PrepareDataForIndexView(user);
                return View(nameof(Index), FillModel(user));
            }

            // test if the newly selected plan is suitable
            bool isFrequentlyExercising = user.Profile!.Pal != PhysicalActivityLevel.Sedentary;
            if (!applicationPlansService.IsMeetingRequirements(newAppPlan, user.Measurement!, isFrequentlyExercising))
            {
                _logger.LogDebug(string.Format(LoggerMessages.NotSuitablePlanChosen, user.UserName));
                ModelState.AddModelError(string.Empty, _controllerLocalizer["NotSuitablePlan"].Value);
                PrepareDataForIndexView(user);
                return View(nameof(Index), FillModel(user));
            }

            user.DeleteScheduledPlans();
            user.AppPlan = newAppPlan;
            plansSchedulingService.CreatePlansFor(user);
            _logger.LogInformation(string.Format(LoggerMessages.ScheduledPlans, user.UserName));

            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(ChangePlan), user.UserName));
                TempData.Add(TempDataMessages.ChangeOfApplicationPlan, _controllerLocalizer["SuccessfulChangeOfApplicationPlan"].Value);
                user = await GetCurrentUserAsync();
            }
            else
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidAttempt, nameof(ChangePlan), user.UserName));
                AddErrors(result);
            }

            PrepareDataForIndexView(user);
            return View(nameof(Index), FillModel(user));
        }

        /// <summary>
        /// POST: App/Settings/ChangeEatingTimeConfig.
        /// </summary>
        public async Task<IActionResult> ChangeEatingTimeConfig(ChangeSettingsModel model)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(ChangeEatingTimeConfig), user.UserName));

            var subModel = model.ChangeEatingTimeConfig;
            if (!subModel.AreTimesFollowingEachOther())
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidModelState, nameof(ChangeEatingTimeConfig)));
                ModelState.AddModelError(string.Empty, _controllerLocalizer["TimesAreNotFollowingEachOther"].Value);
                PrepareDataForIndexView(user);
                return View(nameof(Index), FillModel(user));
            }

            if (!subModel.AreIntervalsBetweenMeals())
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidModelState, nameof(ChangeEatingTimeConfig)));
                ModelState.AddModelError(string.Empty, _controllerLocalizer["TimesDoNotMeetIntervals"].Value);
                PrepareDataForIndexView(user);
                return View(nameof(Index), FillModel(user));
            }

            Dictionary<Meal, HourData> newTimeConfig = new()
            {
                { Meal.Breakfast, subModel.Breakfast },
                { Meal.MidMorningSnack, subModel.MidMorningSnack },
                { Meal.Lunch, subModel.Lunch },
                { Meal.AfternoonSnack, subModel.AfternoonSnack },
                { Meal.Dinner, subModel.Dinner },
                { Meal.Supper, subModel.Supper }
            };

            user.HoursConfig = newTimeConfig;
            user.Meals = GetRescheduledMeals(user.Meals, newTimeConfig);

            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(ChangeEatingTimeConfig), user.UserName));
                TempData.Add(
                    TempDataMessages.ChangeOfEatingTimeConfig,
                    _controllerLocalizer["SuccessfulChangeOfEatingHoursConfig"].Value);
                user = await GetCurrentUserAsync();
            }
            else
            {
                _logger.LogWarning(string.Format(LoggerMessages.InvalidAttempt, nameof(ChangeEatingTimeConfig), user.UserName));
                AddErrors(result);
            }

            PrepareDataForIndexView(user);
            return View(nameof(Index), FillModel(user));
        }

        /// <summary>
        /// POST: App/Settings/SendSensitiveDataToEmail.
        /// </summary>
        public async Task<IActionResult> SendSensitiveDataToEmail([FromServices] IMailService mailService)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            if (user.Profile is null)
            {
                PrepareDataForIndexView(user);
                return View(nameof(Index), FillModel(user));
            }

            Dictionary<string, string> personalData = new();
            IEnumerable<PropertyInfo> userPersonalDataProps = user.GetType()
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            
            IEnumerable<PropertyInfo> profilePersonalDataProps = user.Profile!
                .GetType()
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));

            foreach (PropertyInfo prop in userPersonalDataProps)
            {
                personalData.Add(prop.Name, prop.GetValue(user)?.ToString() ?? "null");
            }

            foreach (PropertyInfo prop in profilePersonalDataProps)
            {
                personalData.Add(prop.Name, prop.GetValue(user.Profile)?.ToString() ?? "null");
            }

            var personalDataSerialized = JsonConvert.SerializeObject(personalData, Formatting.Indented);
            MailRequest personalDataDownload = new(user.Email, _controllerLocalizer["SensitiveDataSubject"].Value, personalDataSerialized);
            await mailService.SendEmailAsync(personalDataDownload);
            _logger.LogDebug(string.Format(LoggerMessages.EmailHasBeenSent, nameof(SendSensitiveDataToEmail)));

            TempData.Add(TempDataMessages.SensitiveDataSent, _controllerLocalizer["SensitiveDataSent"].Value);
            PrepareDataForIndexView(user);
            return View(nameof(Index), FillModel(user));
        }

        /// <summary>
        /// POST: App/Settings/DeleteAccount.
        /// </summary>
        public async Task<IActionResult> DeleteAccount()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(DeleteAccount), user.UserName));

            IdentityResult result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(DeleteAccount), user.UserName));
                return RedirectToAction(
                    nameof(Main.Controllers.HomeController.Index),
                    ControllerNames.Home,
                    new { area = AreaNames.Main });
            }

            _logger.LogDebug(string.Format(LoggerMessages.InvalidAttempt, nameof(DeleteAccount), user.UserName));
            AddErrors(result);
            return View();
        }

        /// <summary>
        /// For each entry in meal plan reschedules using new time config.
        /// </summary>
        /// <param name="oldMeals">initial meal plan.</param>
        /// <param name="newTimeConfig">new user eating times config.</param>
        /// <returns>rescheduled initial meal plan.</returns>
        private static Plan<IScheduledMeal> GetRescheduledMeals(Plan<IScheduledMeal> oldMeals, Dictionary<Meal, HourData> newTimeConfig)
        {
            if (oldMeals is null)
            {
                throw new ArgumentNullException(nameof(oldMeals));
            }

            if (newTimeConfig is null)
            {
                throw new ArgumentNullException(nameof(newTimeConfig));
            }

            Plan<IScheduledMeal> rescheduledMeals = new();
            DateTime newDate;
            foreach (var (date, meal) in oldMeals)
            {
                newDate = new DateTime(
                    date.Year,
                    date.Month,
                    date.Day,
                    newTimeConfig[meal.Type].Hours,
                    newTimeConfig[meal.Type].Minutes,
                    0);
                rescheduledMeals.Add(newDate, meal);
            }
            return rescheduledMeals;
        }

        /// <summary>
        /// Adds current user data to some forms.
        /// </summary>
        /// <param name="user">user who clicked on settings.</param>
        /// <returns>strongly typed model for settings forms.</returns>
        private ChangeSettingsModel FillModel(ApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            ChangeSettingsModel model = new();
            _mapper.Map(user, model.ChangeProfileInformation);
            return model;
        }

        /// <summary>
        /// Indicates whether the given <paramref name="subModel" /> of type <typeparamref name="TSubmodel" /> is valid.
        /// </summary>
        /// <typeparam name="TSubmodel">type of the <paramref name="subModel" />.</typeparam>
        /// <param name="subModel"></param>
        /// <returns>True if <paramref name="subModel" /> is valid; otherwise False.</returns>
        private bool IsSubmodelValid<TSubmodel>(TSubmodel subModel) where TSubmodel : class
        {
            if (subModel is null)
            {
                throw new ArgumentNullException(nameof(subModel));
            }

            Type subModelType = subModel.GetType();
            // delete model suffix
            string subModelName = subModelType.Name[0..^5];

            return subModelType
                .GetProperties()
                .All(prop => ModelState.GetFieldValidationState($"{subModelName}.{prop.Name}") == ModelValidationState.Valid);
        }

        /// <summary>
        /// Prepares esential data for the index view.
        /// </summary>
        /// <param name="user"></param>
        private void PrepareDataForIndexView(ApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            ViewBag.CurrentTimeConfig = user.HoursConfig;
            var translatedAppPlanNames = _sharedLocalizer.ListOf("ApplicationPlans");
            var selectList =
                translatedAppPlanNames.Select(
                    translatedPlanName => new SelectListItem { Value = translatedPlanName, Text = translatedPlanName })
                .ToList();
            ViewBag.TranslatedAppPlans = selectList;
            ViewBag.TranslatedMealNames = _sharedLocalizer.TranslateEnum<Meal>("MealNames");
        }

        /// <summary>
        /// Sends the confirmation link of the email account to the given users email account.
        /// </summary>
        /// <param name="mailService">infrastructure mail service.</param>
        /// <param name="user">user to whom is the mail being sent.</param>
        private async Task SendChangeProfileInformationEmailConfirmation(IMailService mailService, ApplicationUser user)
        {
            string token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            string callbackUrl = Url.Action(
                nameof(Account.Controllers.HomeController.ConfirmEmail),
                ControllerNames.Home,
                new { area = AreaNames.Account, token, email = user.Email },
                protocol: Request.Scheme);

            MailRequest emailRequest = new(
                toEmail: user.Email,
                subject: _controllerLocalizer["EmailConfirmationSubject"].Value,
                body: string.Format(_controllerLocalizer["EmailConfirmationBody"].Value, callbackUrl));

            _ = mailService.SendEmailAsync(emailRequest);
        }
    }
}