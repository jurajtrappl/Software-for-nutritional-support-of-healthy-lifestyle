using Application.Core.Common.Entities;
using Application.Core.Common.Extensions;
using Application.Core.Common.Interfaces;
using Application.Core.Common.Scheduler;
using Application.Core.Constants;
using System;

namespace Application.Core.Nutrition.Schedulers
{
    /// <summary>
    /// Drinking regime scheduler that composes meals according to the weight of the user.
    /// </summary>
    public sealed class WeightBasedDrinkingRegimeScheduler : IDrinkingRegimePlanScheduler
    {
        /// <summary>
        /// Describes how much water is needed to be drink during the day per one kilogram of human weight.
        /// Unit: liter.
        /// </summary>
        private const double LitersOfWaterPerKg = 0.035;

        /// <summary>
        /// User weight.
        /// </summary>
        private double _weight;

        /// <summary>
        /// Set data for the scheduling process.
        /// </summary>
        /// <param name="weight">users weight.</param>
        /// <returns>this instance.</returns>
        public IDrinkingRegimePlanScheduler Configure(double weight)
        {
            _weight = weight;
            return this;
        }

        /// <summary>
        /// Computes one drinking regime plan item for each day of the plan.
        /// </summary>
        /// <returns>Scheduled exercise plan.</returns>
        public Plan<IScheduledDrink> Schedule()
        {
            Plan<IScheduledDrink> plan = new();
            for (var i = 1; i <= SchedulersConstants.PlanLength; i++)
            {
                plan.Add(
                    DateTime.Today.AddDays(i),
                    new ScheduledDrink
                    {
                        Amount = (LitersOfWaterPerKg * _weight).ToTwoDecimals()
                    });
            }

            return plan;
        }
    }
}