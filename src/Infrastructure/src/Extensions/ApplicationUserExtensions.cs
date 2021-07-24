using Application.Core.Common.Enums;
using Application.Core.Constants;
using Application.Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace Application.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ApplicationUser" />.
    /// </summary>
    public static class ApplicationUserExtensions
    {
        /// <summary>
        /// Indicates whether the user has assigned an application plan.
        /// </summary>
        /// <param name="user">user for whom the selection is being tested.</param>
        /// <returns>True if <paramref name="user" /> has selected any application plan; otherwise False.</returns>
        public static bool HasSelectedApplicationPlan(this ApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.AppPlan is not null;
        }

        /// <summary>
        /// Indicates whether the user has generated meal plan on the given date.
        /// </summary>
        /// <param name="user">user for whom the existence is being checked.</param>
        /// <param name="date">date to check for a meal plan.</param>
        /// <returns>True if <paramref name="user" /> has meal plan on <paramref name="date" />; otherwise False.</returns>
        public static bool HasMealPlanOnDate(this ApplicationUser user, DateTime date)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Meals is not null && user.Meals.HasPlanOnDate(date);
        }

        /// <summary>
        /// Indicates whether the user has generated exercise plan on the given date.
        /// </summary>
        /// <param name="user">user for whom the existence is being checked.</param>
        /// <param name="date">date to check for an exercise plan.</param>
        /// <returns>True if <paramref name="user" /> has exercise plan on <paramref name="date" />; otherwise False.</returns>
        public static bool HasExercisePlanOnDate(this ApplicationUser user, DateTime date)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.AppPlan is not null &&
                user.AppPlan == ApplicationPlan.Reduce &&
                user.Exercises is not null &&
                user.Exercises.HasPlanOnDate(date);
        }

        /// <summary>
        /// Indicates whether the user has generated drinking regime plan on the given date.
        /// </summary>
        /// <param name="user">user for whom the existence is being checked.</param>
        /// <param name="date">date to check for a drinking regime plan.</param>
        /// <returns>
        /// True if <paramref name="user" /> has a drinking regime plan on <paramref name="date" />; otherwise False.
        /// </returns>
        public static bool HasDrinkingRegimeOnDate(this ApplicationUser user, DateTime date)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.DrinkingRegime is not null && user.DrinkingRegime.ContainsKey(date);
        }

        /// <summary>
        /// Indicates whether the user has generated any of the plans on the given date.
        /// </summary>
        /// <param name="user">user for whom the existence of any scheduled plan is being tested.</param>
        /// <param name="date">date to check for any plan occurence.</param>
        /// <returns>True if <paramref name="user" /> has any plan on <paramref name="date" />; otherwise False.</returns>
        public static bool HasAnyPlanOnDate(this ApplicationUser user, DateTime date)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.HasMealPlanOnDate(date) || user.HasDrinkingRegimeOnDate(date) || user.HasExercisePlanOnDate(date);
        }

        /// <summary>
        /// Indicates whether the user has not evaluated plan yet.
        /// </summary>
        /// <param name="user">user for whom the evualation check of plan is being tested.</param>
        /// <returns>True if <paramref name="user" /> has not evaluated plan yet; otherwise False.</returns>
        public static bool HasNotEvaluatedPlan(this ApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            // Note:
            // 1. check if for today there is not a plan (meal plan has every application plan)
            // 2. the user must not have deleted plans
            // 3. (1,2) conditions are true for the first day of registration (check if there is a plan tommorrow)
            return !user.HasMealPlanOnDate(today) && (user.Meals.Count > 0) && !user.HasMealPlanOnDate(tomorrow);
        }

        /// <summary>
        /// Indicates whether the user does not use one of the application plans.
        /// </summary>
        /// <param name="user">user for whom the usage is being tested.</param>
        /// <returns>True if <paramref name="user" /> does not use any plan; otherwise False.</returns>
        public static bool IsNotOnPlan(this ApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return user.Meals.Count == 0 &&
                user.DrinkingRegime.Count == 0 &&
                (user.Exercises is null || user.Exercises.Count == 0) &&
                !user.HasMealPlanOnDate(tomorrow);
        }

        /// <summary>
        /// Deletes current application plan data.
        /// </summary>
        /// <param name="user">user for whom the data are being deleted.</param>
        public static void DeletePlanData(this ApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.AppPlan = null;

            // delete generated plans
            user.DeleteScheduledPlans();

            // delete profile and measurement
            user.Profile = null;
            user.Measurement = null;
        }

        /// <summary>
        /// Deletes all plans that are scheduled for the given user.
        /// </summary>
        /// <param name="user">user for whom are the plans being deleted.</param>
        public static void DeleteScheduledPlans(this ApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Meals = new();
            user.Exercises = null;
            user.DrinkingRegime = new();
        }

        /// <summary>
        /// Returns collection of meal types that represent how many meal per day user eats.
        /// </summary>
        /// <param name="user">user for whom the frequency is being calculated.</param>
        public static IReadOnlyList<Meal> GetMealFrequency(this ApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            int mealsPerDayCount = user.Meals.Count / SchedulersConstants.PlanLength;
            return mealsPerDayCount switch
            {
                5 => new List<Meal> { Meal.Breakfast, Meal.MidMorningSnack, Meal.Lunch, Meal.AfternoonSnack, Meal.Dinner },
                6 => new List<Meal> { Meal.Breakfast, Meal.MidMorningSnack, Meal.Lunch, Meal.AfternoonSnack, Meal.Dinner, Meal.Supper },
                _ => new List<Meal> { Meal.Breakfast, Meal.Lunch, Meal.Dinner }
            };
        }
    }
}