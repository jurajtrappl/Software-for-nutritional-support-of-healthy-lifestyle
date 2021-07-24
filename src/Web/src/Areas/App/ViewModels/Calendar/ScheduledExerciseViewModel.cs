using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Web.Areas.App.ViewModels.Calendar
{
    /// <summary>
    /// View model to display content of scheduled exercise item.
    /// </summary>
    public class ScheduledExerciseViewModel
    {
        /// <summary>
        /// Gets or initializes duration.
        /// </summary>
        public int Duration { get; init; }

        /// <summary>
        /// Gets or initializes translated type name.
        /// </summary>
        public string Type { get; init; }

        /// <summary>
        /// Gets or initializes formatted date by the specific culture.
        /// </summary>
        public string CultureFormattedDate { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="ScheduledExerciseViewModel"/>.
        /// </summary>
        public ScheduledExerciseViewModel()
        {
            Type = string.Empty;
            CultureFormattedDate = string.Empty;
        }
    }
}
