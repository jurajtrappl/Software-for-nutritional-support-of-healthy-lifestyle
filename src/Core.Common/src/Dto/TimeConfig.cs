using Application.Core.Common.Constants;
using Application.Core.Common.Enums;
using System;
using System.Collections.Generic;

namespace Application.Core.Common.Dto
{
    /// <summary>
    /// Represents wrapper around data structure that helps meal scheduled plan lookup for specific meals and gives the
    /// user time schedule for eating.
    /// </summary>
    public sealed class TimeConfig
    {
        /// <summary>
        /// Singleton for default time configuration.
        /// </summary>
        public static readonly TimeConfig Default =
            new(
                new Dictionary<Meal, HourData>
                {
                    { Meal.Breakfast, new HourData(EatingTimeDefaults.Breakfast)},
                    { Meal.MidMorningSnack, new HourData(EatingTimeDefaults.MidMorningSnack) },
                    { Meal.Lunch, new HourData(EatingTimeDefaults.Lunch)},
                    { Meal.AfternoonSnack, new HourData(EatingTimeDefaults.AfternoonSnack)},
                    { Meal.Dinner, new HourData(EatingTimeDefaults.Dinner)},
                    { Meal.Supper, new HourData(EatingTimeDefaults.Supper)}
                }
            );

        /// <summary>
        /// Gets the time configuration values.
        /// </summary>
        public Dictionary<Meal, HourData> Values { get; }

        /// <summary>
        /// Initializes a new instance of <seealso cref="TimeConfig" /> class with the given values.
        /// </summary>
        /// <param name="timeConfig"></param>
        public TimeConfig(Dictionary<Meal, HourData> timeConfig)
        {
            Values = timeConfig ?? throw new ArgumentNullException(nameof(timeConfig));
        }
    }
}