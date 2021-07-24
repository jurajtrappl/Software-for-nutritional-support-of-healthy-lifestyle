using Application.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders the calendar.
    /// </summary>
    public sealed class CalendarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ApplicationUser user, List<DateTime> dates)
        {
            CalendarViewModel model = new(user, dates);
            return View(model);
        }
    }
}