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
    /// Formats user plans using ICalendar import standards.
    /// </summary>
    public sealed class ICalFormatter : CalendarFormatter
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
                content
                    .Append(BeginEvent())
                    .Append(CreateDrinkingRegimeEvent(date, drink.Amount))
                    .Append(EndEvent());
            }

            return content;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="exercisePlan">Exercise plan to format.</param>
        protected override StringBuilder CreateExercisePlan(
            Plan<IScheduledExercise> exercisePlan,
            IReadOnlyDictionary<Sport, string> translatedSportNames)
        {
            StringBuilder content = new();
            foreach (var (date, exercise) in exercisePlan)
            {
                content
                    .Append(BeginEvent())
                    .Append(CreateExerciseEvent(date, exercise, translatedSportNames))
                    .Append(EndEvent());
            }

            return content;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="mealPlan">Meal plan to format.</param>
        protected override StringBuilder CreateMealPlan(
            IReadOnlyDictionary<DateTime, IScheduledMeal> mealPlan,
            IReadOnlyDictionary<Meal, string> translatedMealNames)
        {
            StringBuilder content = new();
            foreach (var (date, meal) in mealPlan)
            {
                content
                    .Append(BeginEvent())
                    .Append(CreateMealEvent(date, meal, translatedMealNames))
                    .Append(EndEvent());
            }

            return content;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// ///
        /// <returns>Document footer as <see cref="StringBuilder" />.</returns>
        protected override StringBuilder Footer() => new StringBuilder().AppendLine(IcalendarKeywords.Footer);

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// ///
        /// <returns>Document header as <see cref="StringBuilder" />.</returns>
        protected override StringBuilder Header()
            => new StringBuilder()
                .AppendLine(IcalendarKeywords.HeaderBegin)
                .AppendLine(IcalendarKeywords.HeaderVersion)
                .AppendLine(IcalendarKeywords.HeaderProdId);

        /// <summary>
        /// Adds a start of the event.
        /// </summary>
        private StringBuilder BeginEvent() => new StringBuilder().AppendLine(IcalendarKeywords.EventBegin);

        /// <summary>
        /// Ends an event.
        /// </summary>
        private StringBuilder EndEvent() => new StringBuilder().AppendLine(IcalendarKeywords.EventEnd);

        private StringBuilder CreateDrinkingRegimeEvent(DateTime date, double amount)
            => new StringBuilder()
                .AppendLine($"{IcalendarKeywords.EventDateTimeStart}:{date.ToICalDate()}")
                .AppendLine($"{IcalendarKeywords.EventDateTimeEnd}:{date.ToICalDate()}")
                .AppendLine($"{IcalendarKeywords.EventDateTimeStamp}:{DateTime.Today.ToICalDate()}")
                .AppendLine($"{IcalendarKeywords.EventSummary}:{amount} l");

        /// <summary>
        /// Formats <see cref="IScheduledExercise" /> for the specific <see cref="DateTime" />.
        /// </summary>
        /// <param name="date">Date of the exercise.</param>
        /// <param name="exercise">Exercise to format.</param>
        private StringBuilder CreateExerciseEvent(
            DateTime date,
            IScheduledExercise exercise,
            IReadOnlyDictionary<Sport, string> translatedSportNames)
        {
            if (translatedSportNames.TryGetValue(exercise.Type, out string? exerciseName))
            {
                return new StringBuilder()
                    .AppendLine($"{IcalendarKeywords.EventDateTimeStart}:{date.ToICalDate()}")
                    .AppendLine($"{IcalendarKeywords.EventDateTimeEnd}:{date.ToICalDate()}")
                    .AppendLine($"{IcalendarKeywords.EventDateTimeStamp}:{DateTime.Today.ToICalDate()}")
                    .AppendLine($"{IcalendarKeywords.EventSummary}:{exerciseName} - {exercise.Duration} min.");
            }

            throw new MissingResourcesException(nameof(translatedSportNames), exercise.Type.ToString());
        }

        /// <summary>
        /// Formats <see cref="IScheduledMeal" /> for the specific <see cref="DateTime" />.
        /// </summary>
        /// <param name="date">Date of the meal.</param>
        /// <param name="meal">Meal to format.</param>
        private StringBuilder CreateMealEvent(
            DateTime date,
            IScheduledMeal meal,
            IReadOnlyDictionary<Meal, string> translatedMealNames)
        {
            if (translatedMealNames.TryGetValue(meal.Type, out string? mealName))
            {
                DateTime endTime = date.AddMinutes(EatingTimeDefaults.EatingDuration);

                return new StringBuilder()
                    .AppendLine($"{IcalendarKeywords.EventDateTimeStart}:{date.ToICalTime()}")
                    .AppendLine($"{IcalendarKeywords.EventDateTimeEnd}:{endTime.ToICalTime()}")
                    .AppendLine($"{IcalendarKeywords.EventDateTimeStamp}:{DateTime.Today.ToICalDate()}")
                    .AppendLine($"{IcalendarKeywords.EventSummary}:{mealName}")
                    .AppendLine($"{IcalendarKeywords.EventDescription}:{CreateMealDescription(meal)}");
            }

            throw new MissingResourcesException(nameof(translatedMealNames), meal.Type.ToString());
        }
    }
}