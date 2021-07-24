using Application.Core.Common.Enums;
using System;

namespace Application.Core.Common.Entities
{
    /// <summary>
    /// An entity describing the result of the application plan after its successful completion.
    /// </summary>
    public interface IPlanResult
    {
        /// <summary>
        /// Gets or sets application plan.
        /// </summary>
        ApplicationPlan AppPlan { get; set; }

        /// <summary>
        /// Gets or sets current BMI.
        /// </summary>
        double CurrentBmi { get; set; }

        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        DateTime End { get; set; }

        /// <summary>
        /// Gets or sets previous BMI.
        /// </summary>
        double PreviousBmi { get; set; }

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets total meals count.
        /// </summary>
        int TotalMealsCount { get; set; }

        /// <summary>
        /// Gets or sets total minutes trained.
        /// </summary>
        int TotalMinTrained { get; set; }

        /// <summary>
        /// Gets or sets total kilojoules eaten.
        /// </summary>
        int TotalKjEaten { get; set; }
    }
}