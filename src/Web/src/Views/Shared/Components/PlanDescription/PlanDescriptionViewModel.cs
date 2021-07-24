using Application.Core.Common.Constants;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for the plan description component.
    /// </summary>
    public sealed class PlanDescriptionViewModel
    {
        /// <summary>
        /// Gets or initializes name of the plan description.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets or initializes localizer.
        /// </summary>
        public IHtmlLocalizer Localizer { get; init; }

        /// <summary>
        /// Gets or initializes number of columns.
        /// </summary>
        public int ScreenRatio { get; init; }

        /// <summary>
        /// Gets or initializes names of the columns being displayed.
        /// </summary>
        public string[] InfoColumnNames { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="PlanDescriptionViewModel"/> with the given name,
        /// localizer, screen ratio and column names.
        /// </summary>
        /// <param name="name">plan description name.</param>
        /// <param name="localizer">localizer of shared resources.</param>
        /// <param name="screenRatio">number of columns being displayed.</param>
        /// <param name="infoColumnNames">names of the columns being displayed.</param>
        public PlanDescriptionViewModel(string name, IHtmlLocalizer localizer, int screenRatio, string[] infoColumnNames)
        {
            (Name, Localizer, ScreenRatio, InfoColumnNames) =
                (name, localizer, screenRatio, infoColumnNames);
        }
    }
}