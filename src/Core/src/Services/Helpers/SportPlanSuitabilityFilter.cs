using Application.Core.Attributes;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.NutritionalParameters;
using Application.Core.Services.Base;

namespace Application.Core.Services.LogicProviders
{
    /// <summary>
    /// Provides logic to indicate whether given measurement satisfies the sport application plan.
    /// </summary>
    [ForPlan(ApplicationPlan.Sport)]
    internal sealed class SportPlanSuitabilityFilter : ApplicationPlanSuitabilityFilter
    {
        /// <summary> Indicates whether the given <seealso cref="IMeasurement" /> is suitable for the sport application
        /// plan.
        /// </summary>
        /// <param name="measurement">measurement to test the suitability for.
        /// <param name="isFrequentlyExercising">indicator whether user is exercising regularly.</param>
        /// <returns>True if <paramref name="measurement" /> is suitable; otherwise False.</returns>
        internal override bool IsSuitable(IMeasurement measurement, bool isFrequentlyExercising)
        {
            BodyMassIndex bmi = new(measurement);
            return (bmi.IsNormalWeight() || bmi.IsOverweight()) && isFrequentlyExercising;
        }
    }
}