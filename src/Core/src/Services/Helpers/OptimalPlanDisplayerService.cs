using Application.Core.Attributes;
using Application.Core.Common.Enums;
using Application.Core.Services.Base;

namespace Application.Core.Services.LogicProviders
{
    /// <summary>
    /// Provides logic for displaying scheduled items for user with optimal application plan.
    /// </summary>
    [ForPlan(ApplicationPlan.Optimal)]
    internal sealed class OptimalPlanDisplayerService : ScheduledItemsDisplayer
    {
    }
}