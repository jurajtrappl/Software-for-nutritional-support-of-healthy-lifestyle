using Application.Core.Common.Dto;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Common.Scheduler;
using Application.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Services
{
    /// <summary>
    /// Implementation of UI shopping list service contract.
    /// </summary>
    public sealed class ShoppingListService : IShoppingListService
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <returns><inheritdoc /></returns>
        public Dictionary<string, double> GetShoppingListFor(
            Plan<IScheduledMeal> mealPlan,
            IReadOnlyList<Meal> mealsPerDay,
            Dictionary<Meal, HourData> timeConfiguration,
            DateTime startingDate,
            Meal startingMeal,
            DateTime endingDate,
            Meal endingMeal)
        {
            if (startingDate.IsTheSameDateTimeAs(endingDate))
            {
                return AddIngredients(mealPlan, mealsPerDay, timeConfiguration, startingDate, m => m >= startingMeal && m <= endingMeal)
                    .List;
            }

            ShoppingList list = AddIngredients(mealPlan, mealsPerDay, timeConfiguration, startingDate, m => m >= startingMeal);

            if (startingDate.AreDaysBetween(endingDate))
            {
                list += AddIngredientsFromBetweenDates(mealPlan, mealsPerDay, timeConfiguration, startingDate.AddDays(1), endingDate);
            }

            list += AddIngredients(mealPlan, mealsPerDay, timeConfiguration, endingDate, m => m <= endingMeal);

            return list.List;
        }

        /// <summary>
        /// Returns a list of ingredients and amounts on the given date selected by the given condition.
        /// </summary>
        /// <param name="mealPlan">meal plan that is being searched for ingredients.</param>
        /// <param name="mealsPerDay">list of meal types in one day of the meal plan.</param>
        /// <param name="date">the date of the meals that are processed into the shopping list.</param>
        /// <param name="condition">filters meals to add.</param>
        /// <returns>list of ingredients and amounts as <seealso cref="ShoppingList" />.</returns>
        private static ShoppingList AddIngredients(
            Plan<IScheduledMeal> mealPlan,
            IReadOnlyList<Meal> mealsPerDay,
            Dictionary<Meal, HourData> timeConfiguration,
            DateTime date,
            Func<Meal, bool> condition)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            var mealsToAdd =
                from m in mealsPerDay
                where condition(m)
                select m;

            return ShoppingListFromMeals(mealsToAdd, mealPlan, timeConfiguration, date);
        }

        /// <summary>
        /// Returns a list of ingredients and amounts between the given range of dates.
        /// </summary>
        /// <param name="mealPlan">meal plan that is being searched for ingredients.</param>
        /// <param name="mealsPerDay">list of meal types in one day of the meal plan.</param>
        /// <param name="firstDate">start date of the range.</param>
        /// <param name="lastDate">last date of the range.</param>
        /// <returns>list of ingredients and amounts as <seealso cref="ShoppingList" />.</returns>
        private static ShoppingList AddIngredientsFromBetweenDates(
            Plan<IScheduledMeal> mealPlan,
            IReadOnlyList<Meal> mealsPerDay,
            Dictionary<Meal, HourData> timeConfiguration,
            DateTime firstDate,
            DateTime lastDate)
        {
            ShoppingList shoppingList = new();
            DateTime dateIterator = firstDate;
            while (dateIterator != lastDate)
            {
                shoppingList += ShoppingListFromMeals(mealsPerDay, mealPlan, timeConfiguration, dateIterator);
                dateIterator = dateIterator.AddDays(1);
            }

            return shoppingList;
        }

        /// <summary>
        /// Creates shopping lists for the given date specified by the sequence of meals.
        /// </summary>
        /// <param name="mealsPerDay">list of meal types in one day of the meal plan.</param>
        /// <param name="mealPlan">meal plan that is being searched for ingredients.</param>
        /// <param name="date">the date of the meals that are processed into the shopping list.</param>
        /// <returns></returns>
        private static ShoppingList ShoppingListFromMeals(
            IEnumerable<Meal> mealsPerDay,
            Plan<IScheduledMeal> mealPlan,
            Dictionary<Meal, HourData> timeConfiguration,
            DateTime date)
        {
            if (mealsPerDay is null)
            {
                throw new ArgumentNullException(nameof(mealsPerDay));
            }

            ShoppingList list = new();
            foreach (Meal type in mealsPerDay)
            {
                IScheduledMeal? planOnDate = mealPlan.GetMealOnDate(date, type, timeConfiguration);
                if (planOnDate is not null)
                {
                    list += new ShoppingList(planOnDate.Ingredients);
                }
            }

            return list;
        }

        /// <summary>
        /// Wrapper around ingredients and amounts for convenient work with adding lists together.
        /// </summary>
        private class ShoppingList
        {
            /// <summary>
            /// Ingredients and amounts.
            /// Unit: grams.
            /// </summary>
            public Dictionary<string, double> List { get; set; }

            /// <summary>
            /// Initialize a new instance of <see cref="ShoppingList" /> with the given list of ingredients.
            /// </summary>
            /// <param name="list">list of ingredients and their amounts.</param>
            public ShoppingList(Dictionary<string, double> list)
            {
                List = list ?? throw new ArgumentNullException(nameof(list));
            }

            /// <summary>
            /// Initialize a new instance of <see cref="ShoppingList" /> with empty list of ingredients.
            /// </summary>
            public ShoppingList() : this(new())
            {
            }

            /// <summary>
            /// Sums up ingredients and their amounts.
            /// </summary>
            /// <param name="first">first shopping list to sum up.</param>
            /// <param name="second">second shopping list to sum up.</param>
            /// <returns>summed up shopping lists, in case of the same ingredients the amounts are being summed up.</returns>
            public static ShoppingList operator +(ShoppingList first, ShoppingList second)
            {
                if (first is null)
                {
                    throw new ArgumentNullException(nameof(first));
                }

                if (second is null)
                {
                    throw new ArgumentNullException(nameof(second));
                }

                ShoppingList merged = first;
                foreach (var (ingredientName, amount) in second.List)
                {
                    if (merged.List.ContainsKey(ingredientName))
                    {
                        merged.List[ingredientName] += amount;
                        continue;
                    }

                    merged.List.Add(ingredientName, amount);
                }

                return merged;
            }
        }
    }
}