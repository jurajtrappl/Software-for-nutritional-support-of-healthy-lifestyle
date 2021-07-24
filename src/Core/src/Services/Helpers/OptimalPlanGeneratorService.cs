using Application.Core.Attributes;
using Application.Core.Common.Enums;
using Application.Core.Services.Base;

namespace Application.Core.Services.LogicProviders
{
    /// <summary>
    /// Provides logic for generating scheduled items for user with the optimal application plan.
    /// </summary>
    [ForPlan(ApplicationPlan.Optimal)]
    internal sealed class OptimalPlanGeneratorService : ApplicationPlansGenerator
    {
    }
}