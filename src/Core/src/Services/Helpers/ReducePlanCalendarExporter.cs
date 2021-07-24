using Application.Core.Attributes;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Enums;
using Application.Core.Services.Base;
using System;
using System.Collections.Generic;

namespace Application.Core.Services.Helpers
{
    /// <summary>
    /// Provides logic for exporting calendar for user with the reduce application plan.
    /// </summary>
    [ForPlan(ApplicationPlan.Reduce)]
    internal sealed class ReducePlanCalendarExporter : CalendarExporter
    {
        /// <summary>
        /// Export scheduled plans for the reduce application plan for the given format.
        /// </summary>
        /// <param name="user">user for whom are the calendar data exported.</param>
        /// <param name="format">format of the exported calendar.</param>
        /// <param name="translatedIngredients">localized ingredient names.</param>
        /// <param name="translatedMealNames">localized meal names.</param>
        /// <param name="translatedSportNames">localized sport names.</param>
        /// <returns>exported plans for the reduce application plan as string.</returns>
        public override string Export(
            IApplicationUser user,
            CalendarFormat format,
            IReadOnlyDictionary<DateTime, IScheduledMeal> translatedIngredients,
            IReadOnlyDictionary<Meal, string> translatedMealNames,
            IReadOnlyDictionary<Sport, string> translatedSportNames)
        {
            return ExportPlans(format, user, translatedIngredients, translatedMealNames, translatedSportNames);
        }
    }
}