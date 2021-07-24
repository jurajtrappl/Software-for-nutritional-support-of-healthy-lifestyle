using Microsoft.AspNetCore.Mvc.Localization;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for function description component.
    /// </summary>
    public sealed class FunctionDescriptionViewModel
    {
        /// <summary>
        /// Gets or initializes name of the icon being displayed.
        /// </summary>
        public string IconName { get; init; }

        /// <summary>
        /// Gets or initializes view resource localizer.
        /// </summary>
        public IViewLocalizer Localizer { get; init; }

        /// <summary>
        /// Gets or initializes column name.
        /// </summary>
        public string ColName { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="FunctionDescriptionViewModel"/>
        /// with the given localizer, icon name and column name.
        /// </summary>
        /// <param name="localizer">view resource localizer.</param>
        /// <param name="iconName">name of the icon next to the header.</param>
        /// <param name="colName">name of the column.</param>
        public FunctionDescriptionViewModel(IViewLocalizer localizer, string iconName, string colName)
        {
            (IconName, Localizer, ColName) = (iconName, localizer, colName);
        }
    }
}