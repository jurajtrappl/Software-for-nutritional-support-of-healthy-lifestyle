using Application.Core.Common.Constants;
using System;

namespace Application.Core.Common.Dto
{
    /// <summary>
    /// Helper object that encapsulates calendar logic for year, month and day.
    /// </summary>
    public readonly struct CalendarData
    {
        /// <summary>
        /// Gets year. (AD)
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// Gets month. (1-12)
        /// </summary>
        public int Month { get; }

        /// <summary>
        /// Gets day. (1-31)
        /// </summary>
        public int Day { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="CalendarData" /> with the given year and month as integers.
        /// </summary>
        /// <param name="year">year as integer.</param>
        /// <param name="month">month as integer.</param>
        public CalendarData(int year, int month)
        {
            if (year < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }

            if (month < 0 || month > CalendarConstants.LastMonth)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }

            (Year, Month, Day) = (year, month, 1);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CalendarData" /> with the given year and month.
        /// </summary>
        /// <param name="year">year as string.</param>
        /// <param name="month">month as string.</param>
        public CalendarData(string year, string month)
        {
            if (!int.TryParse(year, out int yearNum))
            {
                throw new ArgumentException(nameof(year));
            }

            if (yearNum < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }

            if (!int.TryParse(month, out int monthNum))
            {
                throw new ArgumentException(nameof(month));
            }

            if (monthNum < 0 || monthNum > CalendarConstants.LastMonth)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }

            (Year, Month, Day) = (yearNum, monthNum, 1);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CalendarData" /> with the given year, month and day.
        /// </summary>
        /// <param name="year">year as string.</param>
        /// <param name="month">month as string.</param>
        /// <param name="day">day as string.</param>
        public CalendarData(string year, string month, string day) : this(year, month)
        {
            if (!int.TryParse(day, out int dayNum))
            {
                throw new ArgumentException(nameof(day));
            }

            if (dayNum < CalendarConstants.MonthFirstDay || dayNum > CalendarConstants.MaximumDaysInMonth)
            {
                throw new ArgumentOutOfRangeException(nameof(day));
            }

            Day = dayNum;
        }

        /// <summary>
        /// Returns next month and year of that next month from the given year and month.
        /// </summary>
        public (int yearOfNextMonth, int nextMonth) NextMonthYearFrom()
        {
            int yearOfNextMonth = Year;
            int nextMonth;

            if (Month == CalendarConstants.LastMonth)
            {
                yearOfNextMonth++;
                nextMonth = CalendarConstants.FirstMonth;
            }
            else
            {
                nextMonth = Month + 1;
            }

            return (yearOfNextMonth, nextMonth);
        }

        /// <summary>
        /// Returns previous month and year of that previous month from the given year and month.
        /// </summary>
        public (int yearOfPreviousMonth, int previousMonth) PreviousMonthYearFrom()
        {
            int yearOfPreviousMonth = Year;
            int previousMonth;

            if (Month == CalendarConstants.FirstMonth)
            {
                yearOfPreviousMonth--;
                previousMonth = CalendarConstants.LastMonth;
            }
            else
            {
                previousMonth = Month - 1;
            }

            return (yearOfPreviousMonth, previousMonth);
        }
    }
}