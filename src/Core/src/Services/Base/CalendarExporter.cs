using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Enums;
using Application.Web.Calendar;
using Application.Web.Calendar.Formatters;
using System;
using System.Collections.Generic;

namespace Application.Core.Services.Base
{
    /// <summary>
    /// Base class for user calendar data exporters.
    /// </summary>
    public abstract class CalendarExporter
    {
        /// <summary>
        /// Map of formatters for the calendar formats.
        /// </summary>
        private static readonly Dictionary<CalendarFormat, Func<CalendarFormatter>> _formatters =
            new()
            {
                { CalendarFormat.Csv, () => new CsvFormatter() },
                { CalendarFormat.Ical, () => new ICalFormatter() },
            };

        /// <summary>
        /// Exports scheduled meals for the given format.
        /// </summary>
        /// <param name="user">user for whom are the calendar data exported.</param>
        /// <param name="format">format of the exported calendar.</param>
        /// <param name="translatedIngredients">localized ingredient names.</param>
        /// <param name="translatedMealNames">localized meal names.</param>
        /// <returns></returns>
        public string ExportMealPlanAs(
            CalendarFormat format,
            IApplicationUser user,
            IReadOnlyDictionary<DateTime, IScheduledMeal> translatedIngredients,
            IReadOnlyDictionary<Meal, string> translatedMealNames)
        {
            return _formatters[format]().ExportCommonPlans(user, translatedIngredients, translatedMealNames);
        }

        /// <summary>
        /// Export all three scheduled plans for the given format.
        /// </summary>
        /// <param name="user">user for whom are the calendar data exported.</param>
        /// <param name="format">format of the exported calendar.</param>
        /// <param name="translatedIngredients">localized ingredient names.</param>
        /// <param name="translatedMealNames">localized meal names.</param>
        /// <param name="translatedSportNames">localized sport names.</param>
        /// <returns>exported plans as string.</returns>
        public string ExportPlans(
            CalendarFormat format,
            IApplicationUser user,
            IReadOnlyDictionary<DateTime, IScheduledMeal> translatedIngredients,
            IReadOnlyDictionary<Meal, string> translatedMealNames,
            IReadOnlyDictionary<Sport, string> translatedSportNames)
        {
            return _formatters[format]().ExportPlans(user, translatedIngredients, translatedMealNames, translatedSportNames);
        }

        /// <summary>
        /// Defines what scheduled plans are exported for the specific application plan.
        /// </summary>
        /// <param name="user">user for whom are the calendar data exported.</param>
        /// <param name="format">format of the exported calendar.</param>
        /// <param name="translatedIngredients">localized ingredient names.</param>
        /// <param name="translatedMealNames">localized meal names.</param>
        /// <param name="translatedSportNames">localized sport names.</param>
        /// <returns>exported plans as string.</returns>
        public virtual string Export(
            IApplicationUser user,
            CalendarFormat format,
            IReadOnlyDictionary<DateTime, IScheduledMeal> translatedIngredients,
            IReadOnlyDictionary<Meal, string> translatedMealNames,
            IReadOnlyDictionary<Sport, string> translatedSportNames)
        {
            return ExportMealPlanAs(format, user, translatedIngredients, translatedMealNames);
        }
    }
}