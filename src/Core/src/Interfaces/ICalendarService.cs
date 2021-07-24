using Application.Core.Common.Entities;
using Application.Core.Common.Entities.Base;
using Application.Core.Common.Enums;
using Application.Core.Common.Scheduler;
using Application.Core.Enums;
using System;
using System.Collections.Generic;

namespace Application.Core.Interfaces
{
    /// <summary>
    /// UI service contract that supports calendar creation and export of data.
    /// </summary>
    public interface ICalendarService
    {
        /// <summary>
        /// Returns calendar data of the given user for the given format.
        /// </summary>
        /// <param name="user">user for whom are the calendar data exported.</param>
        /// <param name="format">format of the exported calendar.</param>
        /// <param name="translatedIngredients">localized ingredient names.</param>
        /// <param name="translatedMealNames">localized meal names.</param>
        /// <param name="translatedSportNames">localized sport names.</param>
        string Export(
            IApplicationUser user,
            CalendarFormat format,
            IReadOnlyDictionary<DateTime, IScheduledMeal> translatedIngredients,
            IReadOnlyDictionary<Meal, string> translatedMealNames,
            IReadOnlyDictionary<Sport, string> translatedSportNames);

        /// <summary>
        /// Return mime type for the given format.
        /// </summary>
        /// <param name="format">format for specifying content type.</param>
        string GetContentType(CalendarFormat format);

        /// <summary>
        /// Generates sequence of dates for the given month in the given year.
        /// </summary>
        /// <param name="year">year of the month.</param>
        /// <param name="month">month for whom the dates are being generated.</param>
        /// <returns>month dates as <seealso cref="IEnumerable{DateTime}" />.</returns>
        IEnumerable<DateTime> GetDatesForMonth(int year, int month);

        /// <summary>
        /// Generates a sequence of week dates starting by the first date.
        /// </summary>
        /// <param name="firstDayOfWeek">first day of the week.</param>
        /// <returns>week dates as <seealso cref="IEnumerable{DateTime}" />.</returns>
        IEnumerable<DateTime> GetDatesForWeek(DateTime firstDayOfWeek);

        /// <summary>
        /// Generates a sequence of plan dates in the given range of dates.
        /// </summary>
        /// <typeparam name="TScheduled">type of plan items.</typeparam>
        /// <param name="plan">plan to</param>
        /// <param name="firstDay">the beginning of the range.</param>
        /// <param name="lastDay">the end of the range.</param>
        /// <returns>plan dates in the range as <seealso cref="IEnumerable{DateTime}" />.</returns>
        IEnumerable<DateTime> GetDatesFromPlan<TScheduled>(
            Plan<TScheduled> plan,
            DateTime firstDay,
            DateTime lastDay) where TScheduled : IPlanItem;

        /// <summary>
        /// Generates a sequence of exercise plan dates with scheduled items exercise activity duration.
        /// </summary>
        /// <param name="exercisePlan">exercise plan from which are dates selected.</param>
        /// <param name="firstDay">the beginning of the range.</param>
        /// <param name="lastDay">the end of the range.</param>
        /// <returns>
        /// exercise plan dates with exercise duration as <seealso cref="IEnumerable{KeyValuePair{DateTime, int}}" />.
        /// </returns>
        IEnumerable<KeyValuePair<DateTime, int>> GetExercisesPlanDatesWithDuration(
            Plan<IScheduledExercise> exercisePlan,
            DateTime firstDay,
            DateTime lastDay);

        /// <summary>
        /// Returns the file names with the suffix for the given format.
        /// </summary>
        /// <param name="format">format for specifying the suffix.</param>
        string GetFileName(CalendarFormat format);
    }
}