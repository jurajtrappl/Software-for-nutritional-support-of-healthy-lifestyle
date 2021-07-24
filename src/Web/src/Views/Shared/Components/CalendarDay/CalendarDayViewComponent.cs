using Application.Core.Common.Extensions;
using Application.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders one day in the calendar.
    /// </summary>
    public sealed class CalendarDayViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ApplicationUser user, DateTime date)
        {
            CalendarDayViewModel model = new(date, date.ToInvariantShortDate(), user);
            return View(model);
        }
    }
}