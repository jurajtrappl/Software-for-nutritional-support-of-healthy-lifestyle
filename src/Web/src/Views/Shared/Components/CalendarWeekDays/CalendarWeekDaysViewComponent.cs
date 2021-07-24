using Application.Web.Areas.App.Controllers;
using Application.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders the header row with names of weekdays in the calendar.
    /// </summary>
    public sealed class CalendarWeekDaysViewComponent : ViewComponent
    {
        /// <summary>
        /// Localizer of <see cref="CalendarController" /> resources.
        /// </summary>
        private readonly IStringLocalizer _controllerLocalizer;

        /// <summary>
        /// Initializes a new instance of <see cref="CalendarWeekDaysViewComponent"/>
        /// with the giver localizer.
        /// </summary>
        /// <param name="controllerLocalizer">calendar controller resource localizer.</param>
        public CalendarWeekDaysViewComponent(IStringLocalizer<CalendarController> controllerLocalizer)
        {
            _controllerLocalizer = controllerLocalizer;
        }

        public IViewComponentResult Invoke(Func<string, string> formatter)
        {
            CalendarWeekDaysViewModel model = new(formatter, _controllerLocalizer.ListOf("WeekDaysNames"));
            return View(model);
        }
    }
}