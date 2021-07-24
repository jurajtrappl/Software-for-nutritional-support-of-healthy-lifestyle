using Application.Core.Common.Constants;
using Application.Core.Common.Dto;
using Application.Core.Common.Entities;
using Application.Core.Common.Entities.Base;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Common.Scheduler;
using Application.Core.Constants;
using Application.Core.Enums;
using Application.Core.Infrastructure;
using Application.Core.Interfaces;
using Application.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Services
{
    /// <summary>
    /// Implementation of UI calendar service contract.
    /// </summary>
    public sealed class CalendarService : ICalendarService
    {
        /// <summary>
        /// Map of file endings for calendar formats.
        /// </summary>
        private static readonly Dictionary<CalendarFormat, string> _fileEndings =
            new()
            {
                { CalendarFormat.Csv, CsvCalendarKeywords.FileFormat },
                { CalendarFormat.Ical, IcalendarKeywords.FileFormat },
            };

        /// <summary>
        /// Map of mime types for calendar formats.
        /// </summary>
        private static readonly Dictionary<CalendarFormat, string> _mimeTypes =
            new()
            {
                { CalendarFormat.Csv, CsvCalendarKeywords.MimeType },
                { CalendarFormat.Ical, IcalendarKeywords.MimeType }
            };

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="user">user for whom are the calendar data exported.</param>
        /// <param name="format">format of the exported calendar.</param>
        /// <param name="translatedIngredients">localized ingredient names.</param>
        /// <param name="translatedMealNames">localized meal names.</param>
        /// <param name="translatedSportNames">localized sport names.</param>
        public string Export(
            IApplicationUser user,
            CalendarFormat format,
            IReadOnlyDictionary<DateTime, IScheduledMeal> translatedIngredients,
            IReadOnlyDictionary<Meal, string> translatedMealNames,
            IReadOnlyDictionary<Sport, string> translatedSportNames)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return LogicProviderSelector<CalendarExporter>.GetInstance((ApplicationPlan)user.AppPlan!)
                .Export(user, format, translatedIngredients, translatedMealNames, translatedSportNames);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="format">format for specifying content type.</param>
        public string GetContentType(CalendarFormat format) => _mimeTypes[format];

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <returns><inheritdoc /></returns>
        public IEnumerable<DateTime> GetDatesForMonth(int year, int month)
        {
            DateTime firstWeekFirstDay = FindFirstDateForMonth(year, month);

            return
                from weekCounter in Enumerable.Range(0, CalendarConstants.WeeksInCalendarComponent)
                let firstWeekDay = firstWeekFirstDay.AddDays(CalendarConstants.DaysInWeek * weekCounter)
                from date in GetDatesForWeek(firstWeekDay)
                select date;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <returns><inheritdoc /></returns>
        public IEnumerable<DateTime> GetDatesForWeek(DateTime firstDayOfWeek)
            => GenerateDates(firstDayOfWeek, CalendarConstants.DaysInWeek);

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <returns><inheritdoc /></returns>
        public IEnumerable<DateTime> GetDatesFromPlan<TScheduled>(
            Plan<TScheduled> plan,
            DateTime firstDay,
            DateTime lastDay)
            where TScheduled : IPlanItem
        {
            if (plan is null)
            {
                throw new ArgumentNullException(nameof(plan));
            }

            return
                from k in plan
                where k.Key.IsInRange(firstDay, lastDay)
                select k.Key;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <returns><inheritdoc /></returns>
        public IEnumerable<KeyValuePair<DateTime, int>> GetExercisesPlanDatesWithDuration(
            Plan<IScheduledExercise> exercises,
            DateTime firstDay,
            DateTime lastDay)
        {
            if (exercises is null)
            {
                throw new ArgumentNullException(nameof(exercises));
            }

            return
                from e in exercises
                where e.Key.IsInRange(firstDay, lastDay)
                select new KeyValuePair<DateTime, int>(e.Key, e.Value.Duration);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="format">format for specifying the suffix.</param>
        public string GetFileName(CalendarFormat format) => $"exported.{_fileEndings[format]}";

        /// <summary>
        /// Returns the <seealso cref="DateTime" /> of the first day in the given month.
        /// </summary>
        /// <param name="year">year of the given month.</param>
        /// <param name="month">month for which the first date is being searched.</param>
        /// <returns>first date of the month as <seealso cref="DateTime" />.</returns>
        private static DateTime FindFirstDateForMonth(int year, int month)
        {
            var (yearOfPreviousMonth, previousMonth) = new CalendarData(year, month).PreviousMonthYearFrom();
            int previousMonthLength = DateTime.DaysInMonth(yearOfPreviousMonth, previousMonth);

            int firstDateDayOfWeek = (int)new DateTime(year, month, CalendarConstants.MonthFirstDay).DayOfWeek;
            return
                firstDateDayOfWeek != 0 ?
                    new(yearOfPreviousMonth, previousMonth, previousMonthLength - firstDateDayOfWeek + 1) :
                    new(year, month, CalendarConstants.MonthFirstDay);
        }

        /// <summary>
        /// Generates sequence of <seealso cref="DateTime" /> of the given size starting from the given date.
        /// </summary>
        /// <param name="dateFrom">start of the sequence.</param>
        /// <param name="count">number of elements.</param>
        /// <returns><seealso cref="DateTime" /> sequence as <seealso cref="IEnumerable{DateTime}" />.</returns>
        private static IEnumerable<DateTime> GenerateDates(DateTime dateFrom, int count)
            => from dayCounter in Enumerable.Range(0, count)
               let date = dateFrom.AddDays(dayCounter)
               select date;
    }
}