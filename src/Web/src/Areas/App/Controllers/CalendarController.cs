using Application.Core.Common.Constants;
using Application.Core.Common.Dto;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Common.Scheduler;
using Application.Core.Enums;
using Application.Core.Interfaces;
using Application.Infrastructure.Entities;
using Application.Web.Areas.App.Calendar.ViewModels;
using Application.Web.Areas.App.Models.Calendar;
using Application.Web.Areas.App.ViewModels.Calendar;
using Application.Web.Constants;
using Application.Web.Extensions;
using Application.Web.Filters;
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
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Areas.App.Controllers
{
    /// <summary>
    /// Calendar actions.
    /// </summary>
    [Area(AreaNames.App)]
    [Authorize]
    [Log]
    public class CalendarController : ExtendedBaseController
    {
        /// <summary>
        /// Calendar service.
        /// </summary>
        private readonly ICalendarService _calendarService;

        /// <summary>
        /// Localizer of controller resources.
        /// </summary>
        private readonly IStringLocalizer _controllerLocalizer;

        /// <summary>
        /// Localizer of ingredient resources.
        /// </summary>
        private readonly IStringLocalizer _ingredientsLocalizer;

        /// <summary>
        /// Web application logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Localizer of shared resources.
        /// </summary>
        private readonly IStringLocalizer _sharedLocalizer;

        public CalendarController(
            ICalendarService calendarService,
            IStringLocalizer<CalendarController> controllerLocalizer,
            IStringLocalizer<Ingredients> ingredientsLocalizer,
            ILogger<CalendarController> logger,
            IStringLocalizer<SharedResources> sharedLocalizer,
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            (_calendarService, _controllerLocalizer, _ingredientsLocalizer, _logger, _sharedLocalizer) =
                (calendarService, controllerLocalizer, ingredientsLocalizer, logger, sharedLocalizer);
        }

        /// <summary>
        /// GET: App/Calendar/Index.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            PrepareDataForIndexView(user);
            return View();
        }

        /// <summary>
        /// POST: App/Calendar/Index.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Index(ExportCalendarModel model)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            // newly registered
            if (user.AppPlan is null || user.Meals.Count == 0)
            {
                PrepareDataForIndexView(user);
                return View();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogDebug(LoggerMessages.InvalidModelState);
                PrepareDataForIndexView(user);
                return View();
            }

            var format = _controllerLocalizer.GetTranslatedValue<CalendarFormat>("CalendarFormats", model.Format);

            string exported = _calendarService.Export(
                user,
                format,
                TranslateMealPlan(user.Meals, _ingredientsLocalizer),
                _sharedLocalizer.TranslateEnum<Meal>("MealNames"),
                _sharedLocalizer.TranslateEnum<Sport>("SportNames"));

            Response.Headers.Add(
                "Content-Disposition",
                $"attachment; filename={_calendarService.GetFileName(format)}");

            byte[] fileContent = Encoding.UTF8.GetBytes(exported);
            return File(fileContent, _calendarService.GetContentType(format));
        }

        /// <summary>
        /// GET: App/Calendar/UpdateMonth.
        /// </summary>
        public async Task<IActionResult> UpdateMonth(string year, string month)
        {
            CalendarData calendarData = new(year, month);
            var dates = _calendarService.GetDatesForMonth(calendarData.Year, calendarData.Month)
                .ToList();

            ApplicationUser user = await GetCurrentUserAsync();

            IEnumerable<DateTime>? exercisePlanDates = null;
            if (user.AppPlan == ApplicationPlan.Reduce)
            {
                exercisePlanDates = _calendarService.GetDatesFromPlan(user.Exercises!, dates[0].Date, dates[^1].Date)
                    .ToList();
            }

            CalendarMonthViewModel model = new()
            {
                MonthsNames = _controllerLocalizer.ListOf("MonthsNames"),
                WeekDaysNames = _controllerLocalizer.ListOf("WeekDaysNames"),
                MonthNum = calendarData.Month,
                Year = calendarData.Year,
                Dates = dates,
                MealPlanDates = _calendarService.GetDatesFromPlan(user.Meals, dates[0].Date, dates[^1].Date).ToList(),
                ExercisePlanDates = exercisePlanDates
            };

            return Json(model);
        }

        /// <summary>
        /// GET: App/Calendar/UpdateWeek
        /// </summary>
        public async Task<IActionResult> UpdateWeek(string year, string month, string startDay)
        {
            CalendarData calendarData = new(year, month, startDay);
            DateTime firstDayOfWeek = new(calendarData.Year, calendarData.Month, calendarData.Day);
            DateTime lastDayOfWeek = firstDayOfWeek.AddDays(CalendarConstants.DaysInWeek);

            ApplicationUser user = await GetCurrentUserAsync();

            IEnumerable<KeyValuePair<DateTime, int>>? exercisePlanDates = null;
            if (user.AppPlan == ApplicationPlan.Reduce)
            {
                exercisePlanDates = _calendarService.GetExercisesPlanDatesWithDuration(user.Exercises!, firstDayOfWeek, lastDayOfWeek)
                    .ToList();
            }

            CalendarWeekViewModel model = new()
            {
                WeekDaysNames = _controllerLocalizer.ListOf("WeekDaysNames"),
                WeekStartDate = firstDayOfWeek.ToCurrentCultureDateString(),
                WeekEndDate = lastDayOfWeek.ToCurrentCultureDateString(),
                Dates = _calendarService.GetDatesForWeek(firstDayOfWeek).ToList(),
                MealPlanDates = _calendarService.GetDatesFromPlan(user.Meals, firstDayOfWeek, lastDayOfWeek).ToList(),
                ExercisePlanDates = exercisePlanDates
            };

            return Json(model);
        }

        /// <summary>
        /// GET: App/Calendar/UpdateList.
        /// </summary>
        public async Task<IActionResult> UpdateList(string year, string month, string day)
        {
            var listViewDates = FindListViewDates(year, month, day);
            ApplicationUser user = await GetCurrentUserAsync();
            
            CalendarListViewModel model = new()
            {
                Dates = listViewDates,
                CultureFormattedDates = listViewDates.Select(d => d.ToCurrentCultureDateString()),
                PlanItems = GetListViewPlanItems(user, listViewDates)
            };

            return Json(model);
        }

        /// <summary>
        /// GET: App/Calendar/ShowDrinkingRegime.
        /// </summary>
        public async Task<IActionResult> ShowDrinkingRegime([FromQuery] string date)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            if (DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateKey))
            {
                if (!user.DrinkingRegime.ContainsKey(dateKey))
                {
                    _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, date));
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { });
                }

                ScheduledDrinkViewModel model = new()
                {
                    Amount = user.DrinkingRegime[dateKey].Amount,
                    CultureFormattedDate = dateKey.ToCurrentCultureDateString()
                };

                return Json(model);
            }

            _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, date));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { });
        }

        /// <summary>
        /// GET: App/Calendar/ShowExercise.
        /// </summary>
        public async Task<IActionResult> ShowExercise([FromQuery] string date)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            if (DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateKey))
            {
                IScheduledExercise? exercise = user.Exercises!.GetItemOnDate(d => d.IsTheSameDateTimeAs(dateKey));
                if (exercise is null)
                {
                    _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, date));
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { });
                }

                var translatedSports = _sharedLocalizer.TranslateEnum<Sport>("SportNames");
                if (translatedSports.TryGetValue(exercise.Type, out string? type))
                {
                    ScheduledExerciseViewModel model = new()
                    {
                        Duration = exercise.Duration,
                        Type = type,
                        CultureFormattedDate = dateKey.ToCurrentCultureDateString()
                    };

                    return Json(model);
                }

                _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInResources, exercise.Type.ToString()));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { });
            }

            _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, date));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { });
        }

        /// <summary>
        /// GET: App/Calendar/ShowConcreteMeal.
        /// </summary>
        public async Task<IActionResult> ShowMeal([FromQuery] string date, [FromQuery] string time)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            if (DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateKey))
            {
                HourData hourData = new(time);
                dateKey = dateKey.AddHours(hourData.Hours)
                    .AddMinutes(hourData.Minutes);

                IScheduledMeal? meal = user.Meals.GetItemOnDate(d => d.IsTheSameDateAs(dateKey) && d.Hour == dateKey.Hour);
                if (meal is null)
                {
                    _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, date));
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { });
                }

                IScheduledMeal translatedMeal = meal.TranslateIngredients(_ingredientsLocalizer);
                var translatedMealNames = _sharedLocalizer.TranslateEnum<Meal>("MealNames");
                if (translatedMealNames.TryGetValue(meal.Type, out string? name))
                {
                    string key = $"{name} - {dateKey.ToCurrentCultureTimeString()}";

                    ScheduledMealViewModel model = new()
                    {
                        MealsData = new() { { key, translatedMeal } },
                        ColNames = _controllerLocalizer.ListOf("IngredientTableColumnNames"),
                        CultureFormattedDate = dateKey.ToCurrentCultureDateString()
                    };

                    return Json(model);
                }

                _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInResources, meal.Type.ToString()));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { });
            }

            _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, date));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { });
        }

        /// <summary>
        /// GET: App/Calendar/ShowMeals.
        /// </summary>
        public async Task<IActionResult> ShowMeals([FromQuery] string date)
        {
            ApplicationUser currentUser = await GetCurrentUserAsync();

            if (DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateKey))
            {
                var translatedMealNames = _sharedLocalizer.TranslateEnum<Meal>("MealNames");

                ScheduledMealViewModel model = new()
                {
                    MealsData = MealsWithTimesForDate(dateKey, currentUser, translatedMealNames, _ingredientsLocalizer),
                    ColNames = _controllerLocalizer.ListOf("IngredientTableColumnNames"),
                    CultureFormattedDate = dateKey.ToCurrentCultureDateString()
                };

                return Json(model);
            }

            _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInView, date));
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { });
        }

        /// <summary>
        /// Localizes ingredient names in the given meal plan.
        /// </summary>
        /// <param name="mealPlan">meal plan with scheduled meals to localize ingredients for.</param>
        /// <param name="ingredientsLocalizer">localizer of ingredient resources.</param>
        /// <returns>Translated <paramref name="mealPlan" />.</returns>
        private static Dictionary<DateTime, IScheduledMeal> TranslateMealPlan(
            Plan<IScheduledMeal> mealPlan,
            IStringLocalizer ingredientsLocalizer)
        {
            Dictionary<DateTime, IScheduledMeal> translatedMealIngredients = new();
            foreach (var (date, meal) in mealPlan)
            {
                translatedMealIngredients.Add(date, meal.TranslateIngredients(ingredientsLocalizer));
            }
            return translatedMealIngredients;
        }

        /// <summary>
        /// Adds essential data to the ViewBag.
        /// </summary>
        /// <param name="user">data source of the essential data.</param>
        private void PrepareDataForIndexView(ApplicationUser user)
        {
            int currentYear = DateTime.Today.Year;
            int currentMonth = DateTime.Today.Month;
            CalendarMonthViewModel calendar = new()
            {
                MonthNum = currentMonth,
                Year = currentYear,
                Dates = _calendarService.GetDatesForMonth(currentYear, currentMonth)
                    .ToList()
            };
            ViewBag.Calendar = calendar;

            var translatedCalendarFormats = _controllerLocalizer.ListOf("CalendarFormats");
            ViewBag.TranslatedCalendarFormats =
                translatedCalendarFormats
                    .Select(type => new SelectListItem { Value = type, Text = type });

            ViewBag.User = user;
        }

        /// <summary>
        /// Returns map with formatted dates and scheduled meals for them on the given date.
        /// </summary>
        /// <param name="date">date of the scheduled meals.</param>
        /// <param name="user">user for whom is the map being created.</param>
        /// <param name="translatedMealNames">current culture names of meals.</param>
        /// <param name="ingredientsLocalizer">current culture names of ingredients.</param>
        private Dictionary<string, IScheduledMeal> MealsWithTimesForDate(
            DateTime date,
            IApplicationUser user,
            IReadOnlyDictionary<Meal, string> translatedMealNames,
            IStringLocalizer ingredientsLocalizer)
        {
            if (translatedMealNames is null)
            {
                throw new ArgumentNullException(nameof(translatedMealNames));
            }

            Dictionary<string, IScheduledMeal> result = new();
            foreach (IScheduledMeal meal in user.Meals.GetOnlyItemsOnDate(date))
            {
                string parsedDate = date.ConstructDateWithTime(meal.Type, user.HoursConfig)
                        .ToCurrentCultureTimeString();

                if (translatedMealNames.TryGetValue(meal.Type, out string? name))
                {
                    result.Add(
                        key: $"{name} - {parsedDate}",
                        value: meal.TranslateIngredients(ingredientsLocalizer));
                }
                else
                {
                    _logger.LogCritical(string.Format(LoggerMessages.InvalidDataInResources, meal.Type.ToString()));
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a sequence of dates for list view to display.
        /// </summary>
        /// <param name="year">year of the first date in sequence as string.</param>
        /// <param name="month">month of the first date in sequence as string.</param>
        /// <param name="day">day of the first date in sequence as string.</param>
        private static List<DateTime> FindListViewDates(string year, string month, string day)
        {
            CalendarData calendarData = new(year, month, day);
            DateTime date = new(calendarData.Year, calendarData.Month, calendarData.Day);
            return Enumerable.Range(0, CalendarConstants.ListViewDays)
                .Select(d => date.AddDays(d))
                .ToList();
        }

        /// <summary>
        /// Returns collection of scheduled items for day for the given user and in the given range.
        /// </summary>
        /// <param name="user">user for whom the data are being displayed.</param>
        /// <param name="dates">range of dates for the list view.</param>
        private static List<OneDayPlanItemsViewModel> GetListViewPlanItems(ApplicationUser user, List<DateTime> dates)
        {
            List<OneDayPlanItemsViewModel> planItems = new();
            DateTime iterator = dates[0];
            DateTime last = dates[^1].AddDays(1);
            while (iterator != last)
            {
                OneDayPlanItemsViewModel planDay = new()
                {
                    DrinkingRegime = user.DrinkingRegime.GetItemOnDate(d => d.IsTheSameDateTimeAs(iterator))!,
                    Exercise = user.Exercises?.GetItemOnDate(d => d.IsTheSameDateTimeAs(iterator)),
                    Meals = user.Meals.GetItemsWithDatesOnDate(iterator)
                        .ToDictionary(k => k.Key, v => v.Value)
                };

                planItems.Add(planDay);
                iterator = iterator.AddDays(1);
            }
            return planItems;
        }
    }
}