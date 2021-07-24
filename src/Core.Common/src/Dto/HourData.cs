using Application.Core.Common.Constants;
using System;

namespace Application.Core.Common.Dto
{
    /// <summary>
    /// Helper object that encapsulates time logic for hours and minutes.
    /// </summary>
    public class HourData
    {
        /// <summary>
        /// Gets or sets hours.
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Gets or sets minutes.
        /// </summary>
        public int Minutes { get; set; }

        /// <summary>
        /// Default initialization.
        /// </summary>
        public HourData() : this(0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of <seealso cref="HourData" /> from string representation of time. Expected
        /// format is either hh:mm or HH:MM.
        /// </summary>
        /// <param name="time">string that contains hours and minutes.</param>
        public HourData(string time)
        {
            if (string.IsNullOrEmpty(time))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(time)));
            }

            var timeData = time.Split(":");

            if (!int.TryParse(timeData[0], out int hour))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.TimeNotValidHours, time));
            }

            if (!int.TryParse(timeData[1], out int minutes))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.TimeNotValidMinutes, time));
            }

            (Hours, Minutes) = (hour, minutes);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HourData" /> with the given hours and minutes.
        /// </summary>
        /// <param name="hours">hours to set.</param>
        /// <param name="minutes">minutes to set.</param>
        public HourData(int hours, int minutes = 0)
        {
            (Hours, Minutes) = (hours, minutes);
        }

        /// <param name="first">left side of the inequality.</param>
        /// <param name="second">right side of the inequality.</param>
        /// <returns>True if <paramref name="first" /> is less than <paramref name="second" />; otherwise False.</returns>
        public static bool operator <(HourData first, HourData second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (first.Hours == second.Hours)
            {
                return first.Minutes < second.Minutes;
            }

            return first.Hours < second.Hours;
        }

        /// <param name="first">left side of the inequality.</param>
        /// <param name="second">right side of the inequality.</param>
        /// <returns>True if <paramref name="first" /> is greater than <paramref name="second" />; otherwise False.</returns>
        public static bool operator >(HourData first, HourData second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (first.Hours == second.Hours)
            {
                return first.Minutes > second.Minutes;
            }

            return first.Hours > second.Hours;
        }

        /// <param name="first">subtrahend.</param>
        /// <param name="second">minuend.</param>
        /// <returns>Difference in minutes between <paramref name="first" /> and <paramref name="second" />.</returns>
        public static int operator -(HourData first, HourData second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            int firstInMinutes = first.Hours * 60 + first.Minutes;
            int secondInMinutes = second.Hours * 60 + second.Minutes;
            return Math.Abs(firstInMinutes - secondInMinutes);
        }
    }
}