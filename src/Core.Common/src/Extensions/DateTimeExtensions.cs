using Application.Core.Common.Constants;
using Application.Core.Common.Dto;
using Application.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Application.Core.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="DateTime" />.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Adds time from string to <seealso cref="DateTime" />.
        /// </summary>
        /// <param name="date">date to add the time data to.</param>
        /// <param name="time">time data to add.</param>
        /// <returns>
        /// <seealso cref="DateTime" /> with added time if the format of time is correct; otherwise throws exception.
        /// </returns>
        public static DateTime AddTime(this DateTime date, string time)
        {
            HourData data = new(time);
            return date.AddHours(data.Hours)
                .AddMinutes(data.Minutes);
        }

        /// <summary>
        /// Indicates whether there are days between the two given dates.
        /// </summary>
        /// <param name="sooner">start date.</param>
        /// <param name="later">end date.</param>
        /// <returns>
        /// True if there is atleast one day between <paramref name="sooner" /> and <paramref name="later" />; otherwise False.
        /// </returns>
        public static bool AreDaysBetween(this DateTime sooner, DateTime later) => (later - sooner).Days > 1;

        /// <summary>
        /// Indicates whether the given date is in the range of two given dates.
        /// </summary>
        /// <param name="date">date to check if its in the range.</param>
        /// <param name="first">start date of the range.</param>
        /// <param name="second">end date of the range.</param>
        /// <returns>
        /// True if <paramref name="date" /> is in between of <paramref name="first" /> and <paramref name="second" />;
        /// otherwise False.
        /// </returns>
        public static bool IsInRange(this DateTime date, DateTime first, DateTime second) =>
            date.Year >= first.Year && date.Year <= second.Year &&
            date.Month >= first.Month && date.Month <= second.Month &&
            date.Date >= first.Date && date.Date <= second.Date;

        /// <summary>
        /// Indicates whether the given date has the same year, month and date as the other given date.
        /// </summary>
        /// <param name="first">first date to check.</param>
        /// <param name="second">second date to check.</param>
        /// <returns>
        /// True if <paramref name="first" /> has the same year, month and date as <paramref name="second" />; otherwise False.
        /// </returns>
        public static bool IsTheSameDateAs(this DateTime first, DateTime second) =>
            first.Date == second.Date && first.Month == second.Month && first.Year == second.Year;

        /// <summary>
        /// Indicates whether the given date has the same date (year, month, date), and time (hours, minutes) as the
        /// other given date.
        /// </summary>
        /// <param name="first">first date to check.</param>
        /// <param name="second">second date to check.</param>
        /// <returns>
        /// True if <paramref name="first" /> has the same date and time as <paramref name="second" />; otherwise False.
        /// </returns>
        public static bool IsTheSameDateTimeAs(this DateTime first, DateTime second) =>
            IsTheSameDateAs(first, second) && first.Hour == second.Hour && first.Minute == second.Minute;

        /// <summary>
        /// Formats the given date using short date format and current culture.
        /// </summary>
        /// <param name="date">date to format.</param>
        /// <returns>Invariant culture short date string.</returns>
        public static string ToCurrentCultureDateString(this DateTime date) =>
            date.ToString(DateTimeFormats.ShortDate, CultureInfo.CurrentCulture);

        /// <summary>
        /// Formats the given date using short time format and current culture.
        /// </summary>
        /// <param name="date">date to format.</param>
        /// <returns>Current culture short time string.</returns>
        public static string ToCurrentCultureTimeString(this DateTime date) =>
            date.ToString(DateTimeFormats.ShortTime, CultureInfo.CurrentCulture);

        /// <summary>
        /// Formats the given date using general date short time format and invariant culture. This produces the format
        /// of database key for <seealso cref="DateTime" />.
        /// </summary>
        /// <param name="date">date to format.</param>
        /// <returns>Invariant culture general date short time string.</returns>
        public static string ToDatabaseKeyString(this DateTime date) =>
            date.ToString(DateTimeFormats.GeneralDateShortTime, CultureInfo.InvariantCulture);

        /// <summary>
        /// Formats the given date using short date format and invariant culture.
        /// </summary>
        /// <param name="date">date to format.</param>
        /// <returns>Invariant culture short date from <paramref name="date" />.</returns>
        public static string ToInvariantShortDate(this DateTime date) =>
            date.ToString(DateTimeFormats.ShortDate, CultureInfo.InvariantCulture);

        /// <summary>
        /// Formats the given date to csv date convention.
        /// </summary>
        /// <param name="date">date to format.</param>
        public static string ToCsvDate(this DateTime date) =>
            date.ToString(DateTimeFormats.ShortDate, new CultureInfo(CultureDescriptors.EnglishWithUSRegion));

        /// <summary>
        /// Formats the given date csv time convention.
        /// </summary>
        /// <param name="date">time to format.</param>
        public static string ToCsvTime(this DateTime date) =>
            date.ToString(DateTimeFormats.ShortTime, new CultureInfo(CultureDescriptors.EnglishWithUSRegion));

        /// <summary>
        /// Formats the given date to ical date convention.
        /// </summary>
        /// <param name="date">date to format.</param>
        public static string ToICalDate(this DateTime date) =>
            date.ToString(DateTimeFormats.ICalDate);

        /// <summary>
        /// Formats the given date ical time convention.
        /// </summary>
        /// <param name="date">time to format.</param>
        public static string ToICalTime(this DateTime date) =>
            date.ToString(DateTimeFormats.ICalTime);

        /// <summary>
        /// Helper method that adds <see cref="HourData" /> to the given <see cref="DateTime" />.
        /// </summary>
        /// <param name="initial">date to which is being hour data added.</param>
        /// <param name="type">key for the time config.</param>
        /// <param name="timeConfig">hour data source.</param>
        /// <returns><paramref name="initial" /> with <see cref="HourData" /> added.</returns>
        public static DateTime ConstructDateWithTime(
            this DateTime initial,
            Meal type,
            Dictionary<Meal, HourData> timeConfig)
        {
            if (timeConfig is null)
            {
                throw new ArgumentNullException(nameof(timeConfig));
            }

            if (timeConfig.ContainsKey(type))
            {
                var dateWithTime = initial.AddHours(timeConfig[type].Hours)
                    .AddMinutes(timeConfig[type].Minutes);
                return dateWithTime;
            }

            return initial;
        }
    }
}