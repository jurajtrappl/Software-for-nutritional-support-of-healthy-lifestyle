using Application.Core.Attributes;
using Application.Core.Common.Enums;
using Application.Core.Services.Base;

namespace Application.Core.Services.Helpers
{
    /// <summary>
    /// Provides logic for exporting calendar for user with the optimal application plan.
    /// </summary>
    [ForPlan(ApplicationPlan.Optimal)]
    internal sealed class OptimalPlanCalendarExporter : CalendarExporter
    {
    }
}