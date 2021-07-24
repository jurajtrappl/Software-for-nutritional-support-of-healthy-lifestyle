using Application.Core.Attributes;
using Application.Core.Common.Enums;
using Application.Core.Services.Base;

namespace Application.Core.Services.LogicProviders
{
    /// <summary>
    /// Provides logic for displaying scheduled items for user with sport application plan.
    /// </summary>
    [ForPlan(ApplicationPlan.Sport)]
    internal sealed class SportPlanDisplayerService : ScheduledItemsDisplayer
    {
    }
}