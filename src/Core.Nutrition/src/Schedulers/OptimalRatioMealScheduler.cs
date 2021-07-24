using Application.Core.Common.Dto;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Common.Interfaces;
using Application.Core.Common.NutritionalParameters;
using Application.Core.Common.Scheduler;
using Application.Core.Constants;
using Application.Core.Nutrition.EatingOccasions.Base;
using Application.Core.Nutrition.Frequencies;
using Application.Core.Nutrition.MealComposers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Nutrition.Schedulers
{
    /// <summary>
    /// Meal scheduler that composes meals according to optimal ratio of macronutrients.
    /// </summary>
    public sealed class OptimalRatioMealScheduler : IMealPlanScheduler
    {
        /// <summary>
        /// Logic of meal composing.
        /// </summary>
        private MealComposer? _mealComposer;

        /// <summary>
        /// Number of meals per day.
        /// </summary>
        private IReadOnlyList<MealOccasion>? _mealFrequency;

        private Dictionary<Meal, HourData>? _eatingTime;

        /// <summary>
        /// Set needed data for the scheduling process.
        /// </summary>
        /// <param name="ingredients">ingredients pool.</param>
        /// <param name="measurement">user measurement.</param>
        /// <param name="profile">user profile data.</param>
        /// <param name="appPlan">user application plan.</param>
        /// <returns></returns>
        public IMealPlanScheduler Configure(IReadOnlyDictionary<string, List<IIngredient>> ingredients, IApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            TotalDailyEnergyExpenditure tee = new(user.Measurement!, user.Profile!);
            _mealFrequency = MealFrequencySelector.Choose((ApplicationPlan)user.AppPlan!, tee);
            _mealComposer =
                new MealComposer(
                    ingredients,
                    MealRequirements.GetMealRequirements(tee, _mealFrequency));
            _eatingTime = user.HoursConfig;

            return this;
        }

        /// <summary>
        /// Creates the meal plan for the length of the plan.
        /// </summary>
        /// <returns>MealOccasion plan containing scheduled foods on specific dates.</returns>
        public Plan<IScheduledMeal> Schedule()
        {
            if (_eatingTime is null)
            {
                throw new NullReferenceException(nameof(_eatingTime));
            }

            if (_mealComposer is null)
            {
                throw new NullReferenceException(nameof(_mealComposer));
            }

            if (_mealFrequency is null)
            {
                throw new NullReferenceException(nameof(_mealFrequency));
            }

            Plan<IScheduledMeal> plan = new();
            for (var i = 1; i <= SchedulersConstants.PlanLength; i++)
            {
                foreach (var scheduledMeal in ScheduleDay())
                {
                    plan.AddMealOnDate(DateTime.Today.AddDays(i), scheduledMeal, _eatingTime);
                }
            }

            return plan;
        }

        /// <summary>
        /// Prepares meals for a day.
        /// </summary>
        /// <returns>Collection of meal types and scheduled food to them.</returns>
        private IEnumerable<IScheduledMeal> ScheduleDay()
        {
            return
                (from meal in _mealFrequency
                 select new ScheduledMeal(meal.Type, _mealComposer!.Compose(meal)))
                .Cast<IScheduledMeal>()
                .ToList();
        }
    }
}