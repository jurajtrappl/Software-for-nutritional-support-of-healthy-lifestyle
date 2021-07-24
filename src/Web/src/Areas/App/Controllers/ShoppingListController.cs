using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Interfaces;
using Application.Infrastructure.Entities;
using Application.Infrastructure.Extensions;
using Application.Web.Areas.App.Models.ShoppingList;
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
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Web.Areas.App.Controllers
{
    /// <summary>
    /// Shopping list summary actions.
    /// </summary>
    [Area(AreaNames.App)]
    [Authorize]
    [Log]
    public class ShoppingListController : ExtendedBaseController
    {
        /// <summary>
        /// Localizer of controller resources.
        /// </summary>
        private readonly IStringLocalizer _controllerLocalizer;

        /// <summary>
        /// Localizer of ingredient names.
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

        public ShoppingListController(
            IStringLocalizer<ShoppingListController> controllerLocalizer,
            IStringLocalizer<Ingredients> ingredientsLocalizer,
            ILogger<ShoppingListController> logger,
            IStringLocalizer<SharedResources> sharedLocalizer,
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            (_controllerLocalizer, _ingredientsLocalizer, _logger, _sharedLocalizer) =
                (controllerLocalizer, ingredientsLocalizer, logger, sharedLocalizer);
        }

        /// <summary>
        /// GET: App/ShoppingList/Index.
        /// </summary>
        public IActionResult Index()
        {
            PrepareDataForIndexView();
            return View();
        }

        /// <summary>
        /// POST: App/ShoppingList/Index.
        /// </summary>
        public async Task<IActionResult> GetShoppingList(
            [FromServices] IShoppingListService shoppingListService,
            [FromForm] PlanIntervalModel model)
        {
            PrepareDataForIndexView();
            ApplicationUser user = await GetCurrentUserAsync();
            _logger.LogInformation(string.Format(LoggerMessages.Attempt, nameof(GetShoppingList), user.UserName));

            // dates
            if (!user.HasMealPlanOnDate(model.StartingDate))
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidAttempt, nameof(GetShoppingList), user.UserName));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { errorMessage = _controllerLocalizer["StartingDateInvalid"].Value });
            }

            if (!user.HasMealPlanOnDate(model.EndingDate))
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidAttempt, nameof(GetShoppingList), user.UserName));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { errorMessage = _controllerLocalizer["EndingDateInvalid"].Value });
            }

            // meal types
            IReadOnlyList<Meal> frequency = user.GetMealFrequency();

            Meal startingMeal = _sharedLocalizer.GetTranslatedValue<Meal>("MealNames", model.StartingMeal);
            if (!frequency.Contains(startingMeal))
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidAttempt, nameof(GetShoppingList), user.UserName));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { errorMessage = _controllerLocalizer["StartingMealInvalid"].Value });
            }

            Meal endingMeal = _sharedLocalizer.GetTranslatedValue<Meal>("MealNames", model.EndingMeal);
            if (!frequency.Contains(endingMeal))
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidAttempt, nameof(GetShoppingList), user.UserName));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { errorMessage = _controllerLocalizer["EndingMealInvalid"].Value });
            }

            if (model.StartingDate.IsTheSameDateAs(model.EndingDate) && startingMeal > endingMeal)
            {
                _logger.LogDebug(string.Format(LoggerMessages.InvalidAttempt, nameof(GetShoppingList), user.UserName));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { errorMessage = _controllerLocalizer["InvalidOrderOfMealTypes"].Value });
            }

            Dictionary<string, double> shoppingList = shoppingListService.GetShoppingListFor(
                user.Meals,
                frequency,
                user.HoursConfig,
                model.StartingDate,
                startingMeal,
                model.EndingDate,
                endingMeal);

            _logger.LogDebug(string.Format(LoggerMessages.SuccessfulAttempt, nameof(GetShoppingList), user.UserName));
            return Json(
                new
                {
                    translatedShoppingList = TranslateShoppingList(shoppingList),
                    colNames = _controllerLocalizer.ListOf("ResultColNames")
                });
        }

        /// <summary>
        /// Translates ingredients names to current culture ingredient names.
        /// </summary>
        private SortedDictionary<string, double> TranslateShoppingList(Dictionary<string, double> initialShoppingList)
        {
            if (initialShoppingList is null)
            {
                throw new ArgumentNullException(nameof(initialShoppingList));
            }

            SortedDictionary<string, double> translatedShoppingList = new();
            foreach (var (name, amount) in initialShoppingList)
            {
                translatedShoppingList.Add(_ingredientsLocalizer[name].Value, amount);
            }
            return translatedShoppingList;
        }

        /// <summary>
        /// Prepares essential data for the index view.
        /// </summary>
        private void PrepareDataForIndexView()
        {
            var translatedMealNames = _sharedLocalizer.ListOf("MealNames");
            var selectList =
                translatedMealNames
                    .Select(
                        translatedMealName => new SelectListItem
                        {
                            Value = translatedMealName,
                            Text = translatedMealName
                        })
                    .ToList();
            ViewBag.TranslatedMealNames = selectList;
        }
    }
}