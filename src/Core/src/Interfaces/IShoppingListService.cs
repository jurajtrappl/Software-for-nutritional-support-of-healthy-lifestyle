using Application.Core.Common.Dto;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Scheduler;
using System;
using System.Collections.Generic;

namespace Application.Core.Interfaces
{
    /// <summary>
    /// UI service contract supporting the clarity of the meal plan in the form of a shopping list overview.
    /// </summary>
    public interface IShoppingListService
    {
        /// <summary>
        /// Returns ingredients and their amount for scheduled meals in the given range.
        /// </summary>
        /// <param name="mealPlan">meal plan from which are ingredients taken.</param>
        /// <param name="mealsPerDay">list of types of meals per day.</param>
        /// <param name="startingDate">the starting date of the plan from which the ingredients are selected.</param>
        /// <param name="startingMeal">the starting meal of the day from which the ingerdients are selected.</param>
        /// <param name="endingDate">the ending date of the plan from which the ingredients are selected.</param>
        /// <param name="endingMeal">the ending meal of the day from which the ingredients are selected.</param>
        /// <returns>ingredient names and their amounts (g) as <seealso cref="Dictionary{string, double}" />.</returns>
        Dictionary<string, double> GetShoppingListFor(
            Plan<IScheduledMeal> mealPlan,
            IReadOnlyList<Meal> mealsPerDay,
            Dictionary<Meal, HourData> timeConfiguration,
            DateTime startingDate,
            Meal startingMeal,
            DateTime endingDate,
            Meal endingMeal);
    }
}