using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Scheduler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Calendar
{
    /// <summary>
    /// Base class for formatters that provides logic for exporting Calendar.
    /// </summary>
    public abstract class CalendarFormatter
    {
        /// <summary>
        /// Produces representation of exercise plan in the desired format.
        /// </summary>
        /// <param name="exercisePlan">Exercise plan to export.</param>
        /// <param name="translatedSportNames">localized sport names.</param>
        /// <returns>Exercise plan as string in the specific calendar format.</returns>
        public string ExportExercisePlan(
            Plan<IScheduledExercise> exercisePlan,
            IReadOnlyDictionary<Sport, string> translatedSportNames)
        {
            return Header()
                .Append(CreateExercisePlan(exercisePlan, translatedSportNames))
                .Append(Footer())
                .ToString();
        }

        /// <summary>
        /// Produces representation of meal plan in the desired format.
        /// </summary>
        /// <param name="user">user for whom calendar data are exported.</param>
        /// <param name="mealPlan">Meal plan to export.</param>
        /// <param name="translatedMealNames">localized meal names.</param>
        /// <returns>Meal plan as string in the specific calendar format.</returns>
        public string ExportCommonPlans(
            IApplicationUser user,
            IReadOnlyDictionary<DateTime, IScheduledMeal> mealPlan,
            IReadOnlyDictionary<Meal, string> translatedMealNames)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            StringBuilder drinkingRegimePlanContent = new();
            StringBuilder mealPlanContent = new();

            Parallel.Invoke(() => drinkingRegimePlanContent = CreateDrinkingRegime(user.DrinkingRegime),
                            () => mealPlanContent = CreateMealPlan(mealPlan, translatedMealNames));

            return Header()
                .Append(drinkingRegimePlanContent)
                .Append(mealPlanContent)
                .Append(Footer())
                .ToString();
        }

        /// <summary>
        /// Produces representation of an exercise and meal plan in the desired format.
        /// </summary>
        /// <param name="user">user for whom calendar data are exported.</param>
        /// <param name="translatedIngredients">localized ingredients names.</param>
        /// <param name="translatedSportNames">localized sport names.</param>
        /// <param name="translatedMealNames">localized meal names.</param>
        /// <returns>Both plans as string in the specific calendar format.</returns>
        public string ExportPlans(
            IApplicationUser user,
            IReadOnlyDictionary<DateTime, IScheduledMeal> translatedIngredients,
            IReadOnlyDictionary<Meal, string> translatedMealNames,
            IReadOnlyDictionary<Sport, string> translatedSportNames)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Exercises is null)
            {
                throw new NullReferenceException(nameof(user.Exercises));
            }

            StringBuilder exercisePlanContent = new();
            StringBuilder mealPlanContent = new();

            Parallel.Invoke(() => exercisePlanContent = CreateExercisePlan(user.Exercises, translatedSportNames),
                            () => mealPlanContent = CreateMealPlan(translatedIngredients, translatedMealNames));

            return Header()
                .Append(exercisePlanContent)
                .Append(mealPlanContent)
                .Append(Footer())
                .ToString();
        }

        /// <summary>
        /// Appends items of drinking regime plan to the formatting content.
        /// </summary>
        /// <param name="drinkingRegime">Drinking plan to format.</param>
        protected abstract StringBuilder CreateDrinkingRegime(Plan<IScheduledDrink> drinkingRegime);

        /// <summary>
        /// Appends items of exercise plan to the formatting content.
        /// </summary>
        /// <param name="exercisePlan">Exercise plan to format.</param>
        protected abstract StringBuilder CreateExercisePlan(
            Plan<IScheduledExercise> exercisePlan,
            IReadOnlyDictionary<Sport, string> translatedSportNames);

        /// <summary>
        /// Appends items of the meal plan to the formatting content.
        /// </summary>
        /// <param name="mealPlan">Meal plan to format.</param>
        protected abstract StringBuilder CreateMealPlan(
            IReadOnlyDictionary<DateTime, IScheduledMeal> mealPlan,
            IReadOnlyDictionary<Meal, string> translatedMealNames);

        /// <summary>
        /// Creates the document footer.
        /// </summary>
        protected abstract StringBuilder Footer();

        /// <summary>
        /// Creates the document header.
        /// </summary>
        protected abstract StringBuilder Header();

        /// <summary>
        /// Formats properties of <see cref="IScheduledMeal" />.
        /// </summary>
        /// <param name="meal">Instance to format.</param>
        /// <returns><see cref="StringBuilder" /> with formatted <see cref="IScheduledMeal" />.</returns>
        protected static StringBuilder CreateMealDescription(IScheduledMeal meal)
        {
            StringBuilder description = new();
            foreach (var (ingredientName, amount) in meal.Ingredients)
            {
                description.Append($"{ingredientName} - {amount} g; ");
            }

            // delete last semicolon
            description.Length--;

            return description;
        }
    }
}