using Application.Core.Attributes;
using Application.Core.Common.Enums;
using Application.Core.Services.Base;

namespace Application.Core.Services.Helpers
{
    /// <summary>
    /// Provides logic for exporting calendar for user with the sport application plan.
    /// </summary>
    [ForPlan(ApplicationPlan.Sport)]
    internal sealed class SportPlanCalendarExporter : CalendarExporter
    {
    }
}