using Application.Core.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders card with meal content on homepage.
    /// </summary>
    public sealed class ScheduledMealCardViewComponent : ViewComponent
    {
        /// <summary>
        /// Formats hours or minutes to two places.
        /// </summary>
        /// <param name="value">to format.</param>
        private static string FormatTime(int value) => value < 10 ? $"0{value}" : $"{value}";

        public IViewComponentResult Invoke(DateTime date, string translatedMealName)
        {
            ScheduledMealCardViewModel model = new(
                date.ToDatabaseKeyString(),
                FormatTime(date.Hour) + ":" + FormatTime(date.Minute),
                translatedMealName);
            return View(model);
        }
    }
}