using Application.Core.Common.Entities;

namespace Application.Core.Services.Base
{
    /// <summary>
    /// Base class for logic types that indicate whether given measurement satisfies the specific application plan.
    /// </summary>
    internal abstract class ApplicationPlanSuitabilityFilter
    {
        /// <summary>
        /// Checks whether a given anthropometric is appropriate for the application plan.
        /// </summary>
        /// <param name="measurement">Body mass index.</param>
        /// <param name="isFrequentlyExercising">indicator whether user is exercising regularly.</param>
        /// <returns>True if <paramref name="measurement" /> is suitable for the plan; otherwise False.</returns>
        internal abstract bool IsSuitable(IMeasurement measurement, bool isFrequentlyExercising);
    }
}