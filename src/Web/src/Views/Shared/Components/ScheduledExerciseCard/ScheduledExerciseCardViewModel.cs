using Application.Core.Common.Constants;
using System;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for the scheduled exercise card component.
    /// </summary>
    public sealed class ScheduledExerciseCardViewModel
    {
        /// <summary>
        /// Gets or initializes exercise duration in minutes.
        /// </summary>
        public int Duration { get; init; }

        /// <summary>
        /// Gets or initializes during day localized text.
        /// </summary>
        public string DuringDay { get; init; }

        /// <summary>
        /// Gets or initializes translated sport type.
        /// </summary>
        public string TranslatedSportName { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="ScheduledExerciseCardViewModel"/> with the given
        /// duration, during day text and translated sport name.
        /// </summary>
        /// <param name="duration">exercise duration in minutes.</param>
        /// <param name="duringDay">localized text saying during day in english.</param>
        /// <param name="translatedSportName">translated sport name.</param>
        public ScheduledExerciseCardViewModel(int duration, string duringDay, string translatedSportName)
        {
            (Duration, DuringDay, TranslatedSportName) = (duration, duringDay, translatedSportName);
        }
    }
}