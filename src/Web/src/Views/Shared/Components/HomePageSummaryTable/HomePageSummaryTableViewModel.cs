using Microsoft.AspNetCore.Mvc.Localization;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for the home page summary table component.
    /// </summary>
    public sealed class HomePageSummaryTableViewModel
    {
        /// <summary>
        /// Gets or initializes view resource localizer.
        /// </summary>
        public IViewLocalizer Localizer { get; init; }

        /// <summary>
        /// Gets or initializes name of the table.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="HomePageSummaryTableViewModel"/>
        /// with the given localizer and name.
        /// </summary>
        /// <param name="localizer">view resource localizer.</param>
        /// <param name="name">name of the table.</param>
        public HomePageSummaryTableViewModel(IViewLocalizer localizer, string name)
        {
            (Name, Localizer) = (name, localizer);
        }
    }
}