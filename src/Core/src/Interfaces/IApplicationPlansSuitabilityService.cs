using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using System.Collections.Generic;

namespace Application.Core.Interfaces
{
    /// <summary>
    /// UI service supporting filtering suitable users for different application plans.
    /// </summary>
    public interface IApplicationPlansSuitabilityService
    {
        /// <summary>
        /// Determines whether the given anthropometric is suitable for the given application plan.
        /// </summary>
        /// <param name="applicationPlan">application plan to check against.</param>
        /// <param name="measurement">measurement for whom the suitability is being checked.</param>
        /// <param name="isFrequentlyExercising">indicator whether user is exercising regularly.</param>
        /// <returns>True if <paramref name="measurement" /> is suitable; otherwise False.</returns>
        bool IsMeetingRequirements(ApplicationPlan applicationPlan, IMeasurement measurement, bool isFrequentlyExercising);

        /// <summary>
        /// Returns sequence of application plans that a user with the given measurement can choose.
        /// </summary>
        /// <param name="measurement">measurement for whom the suitability is being checked.</param>
        /// <param name="isFrequentlyExercising">indicator whether user is exercising regularly.</param>
        IEnumerable<ApplicationPlan> FindSuitablePlans(IMeasurement measurement, bool isFrequentlyExercising);
    }
}