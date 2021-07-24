using Application.Core.Common.Entities;
using Application.Core.Exercise.Schedulers;
using Application.Core.Nutrition.Schedulers;
using System;
using System.Collections.Generic;

namespace Application.Web.Areas.App.Calendar.ViewModels
{
    /// <summary>
    /// Model for all scheduled items that can user have within one day.
    /// </summary>
    public sealed class OneDayPlanItemsViewModel
    {
        /// <summary>
        /// Gets or initializes scheduled drinking regime.
        /// </summary>
        public IScheduledDrink DrinkingRegime { get; init; }

        /// <summary>
        /// Gets or initializes scheduled exercise.
        /// </summary>
        public IScheduledExercise? Exercise { get; init; }

        /// <summary>
        /// Gets or initializes map of datetime object and meals.
        /// </summary>
        public IReadOnlyDictionary<DateTime, IScheduledMeal> Meals { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="OneDayPlanItemsViewModel"/>.
        /// </summary>
        public OneDayPlanItemsViewModel()
        {
            DrinkingRegime = new ScheduledDrink();
            Meals = new Dictionary<DateTime, IScheduledMeal>();
        }
    }
}