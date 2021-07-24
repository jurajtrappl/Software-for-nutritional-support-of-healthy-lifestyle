using Application.Core.Common.Enums;
using Application.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Collections.Generic;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for plan history result component.
    /// </summary>
    public sealed class PlanHistoryResultViewModel
    {
        /// <summary>
        /// Gets or initializes start date.
        /// </summary>
        public string Start { get; init; }

        /// <summary>
        /// Gets or initializes end date.
        /// </summary>
        public string End { get; init; }

        /// <summary>
        /// Gets or initializes plan history.
        /// </summary>
        public PlanResult PlanHistory { get; init; }

        /// <summary>
        /// Gets or initializes view localizer.
        /// </summary>
        public IViewLocalizer Localizer { get; init; }

        /// <summary>
        /// Gets or initializes map between application plans and their translations for the specific culture.
        /// </summary>
        public IReadOnlyDictionary<ApplicationPlan, string> TranslatedPlanNames { get; init; }

        /// <summary>
        /// Initialize new instance of <see cref="PlanHistoryResultViewModel"/> with the given
        /// start, end, plan history result, view localizer and translated plan names.
        /// </summary>
        /// <param name="start">start date.</param>
        /// <param name="end">end date.</param>
        /// <param name="planHistory">plan history result data.</param>
        /// <param name="localizer">localizer of view resources.</param>
        /// <param name="translatedPlanNames">map between application plans and their translations for the specific culture</param>
        public PlanHistoryResultViewModel(
            string start,
            string end,
            PlanResult planHistory,
            IViewLocalizer localizer,
            IReadOnlyDictionary<ApplicationPlan, string> translatedPlanNames)
        {
            (Start, End, PlanHistory, Localizer, TranslatedPlanNames) =
                (start, end, planHistory, localizer, translatedPlanNames);
        }
    }
}