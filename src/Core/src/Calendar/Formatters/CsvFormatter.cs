using Application.Core.Common.Constants;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Common.Scheduler;
using Application.Core.Constants;
using Application.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Web.Calendar.Formatters
{
    /// <summary>
    /// Formats user plans using csv import standards by Google.
    /// </summary>
    public sealed class CsvFormatter : CalendarFormatter
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="drinkingRegime">Drinking regime to format.</param>
        /// <returns><paramref name="drinkingRegime" /> formatted as <see cref="StringBuilder" />.</returns>
        protected override StringBuilder CreateDrinkingRegime(Plan<IScheduledDrink> drinkingRegime)
        {
            StringBuilder content = new();
            foreach (var (date, drink) in drinkingRegime)
            {
                content.AppendLine(
                    $"{drink.Amount} l," +
                    $"{date.ToCsvDate()}," +
                    $"," +
                    $"{date.ToCsvDate()}," +
                    $"," +
                    $",");
            }

            return content;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="exercisePlan">Exercise plan to format.</param>
        /// <param name="translatedSportNames">localized sport names.</param>
        protected override StringBuilder CreateExercisePlan(
            Plan<IScheduledExercise> exercisePlan,
            IReadOnlyDictionary<Sport, string> translatedSportNames)
        {
            StringBuilder content = new();
            foreach (var (date, exercise) in exercisePlan)
            {
                if (translatedSportNames.TryGetValue(exercise.Type, out string? sportName))
                {
                    content.AppendLine(
                        $"{sportName} - {exercise.Duration} min.," +
                        $"{date.ToCsvDate()}," +
                        $"," +
                        $"{date.ToCsvDate()}," +
                        $"," +
                        $",");
                }
                else
                {
                    throw new MissingResourcesException(nameof(translatedSportNames), exercise.Type.ToString());
                }
            }

            return content;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="mealPlan">Meal plan to format.</param>
        protected override StringBuilder CreateMealPlan(
            IReadOnlyDictionary<DateTime, IScheduledMeal> translatedMealPlan,
            IReadOnlyDictionary<Meal, string> translatedMealNames)
        {
            StringBuilder content = new();
            foreach (var (date, meal) in translatedMealPlan)
            {
                if (translatedMealNames.TryGetValue(meal.Type, out string? mealName))
                {
                    DateTime endTime = date.AddMinutes(EatingTimeDefaults.EatingDuration);

                    content.AppendLine(
                        $"{mealName}," +
                        $"{date.ToCsvDate()}," +
                        $"{date.ToCsvTime()}," +
                        $"{date.ToCsvDate()}," +
                        $"{endTime.ToCsvTime()}," +
                        $"{CreateMealDescription(meal)}");
                }
                else
                {
                    throw new MissingResourcesException(nameof(translatedMealNames), meal.Type.ToString());
                }
            }

            return content;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <returns>Document footer as <see cref="StringBuilder" />.</returns>
        protected override StringBuilder Footer() => new();

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// ///
        /// <returns>Document header as <see cref="StringBuilder" />.</returns>
        protected override StringBuilder Header()
            => new StringBuilder()
                .AppendLine($"{CsvCalendarKeywords.HeaderSubjectColumn}," +
                            $"{CsvCalendarKeywords.HeaderStartDateColumn}," +
                            $"{CsvCalendarKeywords.HeaderTimeStartColumn}," +
                            $"{CsvCalendarKeywords.HeaderEndDateColumn}," +
                            $"{CsvCalendarKeywords.HeaderEndTimeColumn}," +
                            $"{CsvCalendarKeywords.HeaderDescriptionColumn}");
    }
}