using Application.Core.Common.Dto;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.Scheduler;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Application.Infrastructure.Entities
{
    /// <summary>
    /// Represents database entity of user of the application.
    /// </summary>
    public sealed class ApplicationUser : MongoIdentityUser, IApplicationUser
    {
        /// <summary>
        /// Gets or sets list of allergens.
        /// </summary>
        public List<int> Allergens { get; set; } = new();

        /// <summary>
        /// Gets or sets application plan.
        /// </summary>
        public ApplicationPlan? AppPlan { get; set; }

        /// <summary>
        /// Gets or sets drinking regime plan.
        /// </summary>
        public Plan<IScheduledDrink> DrinkingRegime { get; set; } = new();

        /// <summary>
        /// Gets or sets the exercise plan.
        /// </summary>
        public Plan<IScheduledExercise>? Exercises { get; set; }

        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets history.
        /// </summary>
        public List<IPlanResult> History { get; set; } = new();

        /// <summary>
        /// Gets or sets eating time configuration.
        /// </summary>
        public Dictionary<Meal, HourData> HoursConfig { get; set; } = new();

        /// <summary>
        /// Gets or sets last name.
        /// </summary>
        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the meal plan.
        /// </summary>
        public Plan<IScheduledMeal> Meals { get; set; } = new();

        /// <summary>
        /// Gets or sets measurement.
        /// </summary>
        public IMeasurement? Measurement { get; set; }

        /// <summary>
        /// Gets or sets profile information.
        /// </summary>
        public IProfile? Profile { get; set; }
    }
}