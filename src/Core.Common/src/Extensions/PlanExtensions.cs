using Application.Core.Common.Dto;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Nutrients;
using Application.Core.Common.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Common.Extensions
{
    /// <summary>
    /// Defines extension methods for <seealso cref="Plan{TScheduled}" />
    /// </summary>
    public static class PlanExtensions
    {
        /// <summary>
        /// Returns total number of minutes practiced.
        /// </summary>
        /// <param name="plan">exercise plan.</param>
        public static int TotalExercisedTime(this Plan<IScheduledExercise> plan)
            => plan.Values.Sum(ex => ex.Duration);

        /// <summary>
        /// Returns total number of meals eaten.
        /// </summary>
        /// <param name="plan">meal plan.</param>
        public static int TotalMealsCount(this Plan<IScheduledMeal> plan)
            => plan.Values.Count;

        /// <summary>
        /// Returns total number of kilojoules eaten.
        /// </summary>
        /// <param name="plan">meal plan.</param>
        public static int TotalKilojouleEaten(this Plan<IScheduledMeal> plan)
        {
            double totalCalories = 0;
            foreach (var meal in plan.Values)
            {
                foreach (var macronutrient in (MacroNutrient[])Enum.GetValues(typeof(MacroNutrient)))
                {
                    totalCalories += MacroNutrientConverterContext.FromGramsToKj(
                        macronutrient,
                        meal.MacroNutrients[macronutrient]);
                }
            }

            return (int)totalCalories;
        }

        /// <summary>
        /// Adds a scheduled meal item to the plan on the given date.
        /// </summary>
        /// <param name="plan">meal plan where a scheduled meal is added.</param>
        /// <param name="onlyDate">date when the scheduled meal is added.</param>
        /// <param name="meal">scheduled meal to add.</param>
        public static void AddMealOnDate(
            this Plan<IScheduledMeal> plan,
            DateTime onlyDate,
            IScheduledMeal meal,
            Dictionary<Meal, HourData> timeConfig)
        {
            plan.Add(onlyDate.ConstructDateWithTime(meal.Type, timeConfig), meal);
        }

        /// <summary>
        /// Returns a scheduled meal for the given date and meal type.
        /// </summary>
        /// <param name="plan">scheduled meal items.</param>
        /// <param name="onlyDate">date when is the scheduled meal.</param>
        /// <param name="mealType">type of the scheduled meal.</param>
        /// <returns><seealso cref="IScheduledMeal" /> on the given date and meal type.</returns>
        public static IScheduledMeal? GetMealOnDate(
            this Plan<IScheduledMeal> plan,
            DateTime onlyDate,
            Meal type,
            Dictionary<Meal, HourData> timeConfig)
        {
            DateTime dateWithTime = onlyDate.ConstructDateWithTime(type, timeConfig);

            if (plan.ContainsKey(dateWithTime))
            {
                return plan[dateWithTime];
            }

            return null;
        }
    }
}