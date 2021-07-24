using Application.Core.Common.Enums;
using System;

namespace Application.Core.Attributes
{
    /// <summary>
    /// Describes the classes that provide the implementation logic for a given application plan.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ForPlanAttribute : Attribute
    {
        /// <summary>
        /// Gets the application plan.
        /// </summary>
        public ApplicationPlan AppPlan { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ForPlanAttribute" /> with the given application plan.
        /// </summary>
        /// <param name="appPlan">Application plan for the logic type.</param>
        public ForPlanAttribute(ApplicationPlan appPlan)
        {
            AppPlan = appPlan;
        }
    }
}