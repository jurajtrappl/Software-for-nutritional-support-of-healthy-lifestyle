using Application.Core.Common.Entities;
using System.Collections.Generic;

namespace Application.Web.Areas.App.ViewModels.History
{
    /// <summary>
    /// View model to display result of previous finished application plans.
    /// </summary>
    public sealed class HistoryListViewModel
    {
        /// <summary>
        /// Gets or initializes history.
        /// </summary>
        public List<IPlanResult> History { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="HistoryListViewModel"/>.
        /// </summary>
        public HistoryListViewModel()
        {
            History = new();
        }
    }
}
