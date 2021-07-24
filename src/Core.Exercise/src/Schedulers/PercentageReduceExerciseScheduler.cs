using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Extensions;
using Application.Core.Common.Interfaces;
using Application.Core.Common.Scheduler;
using Application.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Exercise.Schedulers
{
    /// <summary>
    /// Exercise scheduler that calculates the duration of activity by pre-calculated planned loss of pure fat.
    /// </summary>
    public sealed class PercentageReduceExerciseScheduler : IExercisePlanScheduler
    {
        /// <summary>
        /// Weight of the user.
        /// </summary>
        private double _weight;

        /// <summary>
        /// List of exercises for the scheduling process.
        /// </summary>
        private IReadOnlyList<IExercise> _exercises = new List<IExercise>();

        /// <summary>
        /// List of activity types.
        /// </summary>
        private static readonly Sport[] _sports = (Sport[])Enum.GetValues(typeof(Sport));

        /// <summary>
        /// Number of kilojoules to reduce after completion of the plan.
        /// </summary>
        private double TotalKjToReduce =>
            _weight * SchedulersConstants.PureFatReduce * SchedulersConstants.OneKiloFatKj;

        /// <summary>
        /// Sets needed data for the scheduling process.
        /// </summary>
        /// <param name="weight">weight of the user.</param>
        /// <param name="exercises">list of exercise activity.</param>
        /// <returns></returns>
        public IExercisePlanScheduler Configure(double weight, IReadOnlyList<IExercise> exercises)
        {
            (_weight, _exercises) = (weight, exercises);
            return this;
        }

        /// <summary>
        /// Computes one exercise with its duration for each day of the plan.
        /// </summary>
        /// <returns>Scheduled exercise plan.</returns>
        public Plan<IScheduledExercise> Schedule()
        {
            Plan<IScheduledExercise> exercisePlan = new();
            for (var i = 1; i < SchedulersConstants.PlanLength; i++)
            {
                exercisePlan[DateTime.Today.AddDays(i)] =
                    ScheduleOne(TotalKjToReduce / SchedulersConstants.PlanLength);
            }

            return exercisePlan;
        }

        /// <summary>
        /// Picks a random exercise and calculates the duration in minutes that the user needs to spend to reduce the
        /// required amount of kilojoules.
        /// </summary>
        /// <param name="kjToReduce">The required amount of kilojoules to reduce per one exercise.</param>
        /// <returns>Scheduled exercise activity.</returns>
        private IScheduledExercise ScheduleOne(double kjToReduce)
        {
            IExercise randomExercise = _exercises.SelectRandom();

            return new ScheduledExercise
            {
                Duration = DurationForOneExercise(kjToReduce, _weight, randomExercise),
                Type = _sports.First(s => s.ToString() == randomExercise.Name)
            };
        }

        /// <summary>
        /// Calculates the duration in minutes of the given exercise type to reduce the given amount of kilojoules.
        /// </summary>
        /// <param name="kjToReduce">kJ to reduce practising the <paramref name="exercise" />.</param>
        /// <param name="weight">weight of the user.</param>
        /// <param name="exercise">type of the activity.</param>
        /// <returns>int number of minutes.</returns>
        private static int DurationForOneExercise(double kjToReduce, double weight, IExercise exercise)
            => (int)Math.Ceiling(kjToReduce / (exercise.KjPerKgPerMin * weight));
    }
}