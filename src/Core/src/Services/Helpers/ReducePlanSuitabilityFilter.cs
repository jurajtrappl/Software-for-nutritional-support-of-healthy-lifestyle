using Application.Core.Attributes;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Common.NutritionalParameters;
using Application.Core.Services.Base;

namespace Application.Core.Services.LogicProviders
{
    /// <summary>
    /// Provides logic to indicate whether given measurement satisfies the reduce application plan.
    /// </summary>
    [ForPlan(ApplicationPlan.Reduce)]
    internal sealed class ReducePlanSuitabilityFilter : ApplicationPlanSuitabilityFilter
    {
        /// <summary>
        /// Indicates whether the given <seealso cref="IMeasurement" /> is suitable for the reduce application plan.
        /// </summary>
        /// <param name="measurement">measurement to test the suitability for.</param>
        /// <returns>True if <paramref name="measurement" /> is suitable; otherwise False.</returns>
        internal override bool IsSuitable(IMeasurement measurement, bool _) =>
            new BodyMassIndex(measurement).IsOverweight();
    }
}