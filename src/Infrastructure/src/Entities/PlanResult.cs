using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Application.Infrastructure.Entities
{
    /// <summary>
    /// Represents database entity of result of plan completion.
    /// </summary>
    public sealed class PlanResult : IPlanResult
    {
        /// <summary>
        /// Gets or sets application plan.
        /// </summary>
        [BsonRequired]
        public ApplicationPlan AppPlan { get; set; }

        /// <summary>
        /// Gets or sets BMI index value at the end of the plan.
        /// </summary>
        [BsonRequired]
        public double CurrentBmi { get; set; }

        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        [BsonRequired]
        public DateTime End { get; set; }

        /// <summary>
        /// Gets or sets BMI index value at the beginning of the plan.
        /// </summary>
        [BsonRequired]
        public double PreviousBmi { get; set; }

        /// <summary>
        /// Gets or sets the number of total meals eaten.
        /// </summary>
        [BsonRequired]
        public int TotalMealsCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of minutes exercised.
        /// </summary>
        [BsonRequired]
        public int TotalMinTrained { get; set; }

        /// <summary>
        /// Gets or sets the total number of kilojoules eaten in meals.
        /// </summary>
        [BsonRequired]
        public int TotalKjEaten { get; set; }

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        [BsonRequired]
        public DateTime Start { get; set; }
    }
}