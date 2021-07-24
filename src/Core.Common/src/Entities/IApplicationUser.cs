using Application.Core.Common.Dto;
using Application.Core.Common.Enums;
using Application.Core.Common.Scheduler;
using System.Collections.Generic;

namespace Application.Core.Common.Entities
{
    /// <summary>
    /// An entity describing the user of the application.
    /// </summary>
    public interface IApplicationUser
    {
        /// <summary>
        /// Gets or sets allergens (food allergies).
        /// </summary>
        List<int> Allergens { get; set; }

        /// <summary>
        /// Gets or sets application plan.
        /// </summary>
        ApplicationPlan? AppPlan { get; set; }

        /// <summary>
        /// Gets or sets drinking regime.
        /// </summary>
        Plan<IScheduledDrink> DrinkingRegime { get; set; }

        /// <summary>
        /// Gets or sets exercise plan.
        /// </summary>
        Plan<IScheduledExercise>? Exercises { get; set; }

        /// <summary>
        /// Gets or sets history of application plan results.
        /// </summary>
        List<IPlanResult> History { get; set; }

        /// <summary>
        /// Gets or sets hours when is the user eating meals.
        /// </summary>
        Dictionary<Meal, HourData> HoursConfig { get; set; }

        /// <summary>
        /// Gets or sets meal plan.
        /// </summary>
        Plan<IScheduledMeal> Meals { get; set; }

        /// <summary>
        /// Gets or sets measurement.
        /// </summary>
        IMeasurement? Measurement { get; set; }

        /// <summary>
        /// Gets or sets profile.
        /// </summary>
        IProfile? Profile { get; set; }
    }
}