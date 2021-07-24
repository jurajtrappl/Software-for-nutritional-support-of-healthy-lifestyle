using Application.Core.Common.Dto;

namespace Application.Core.Common.Constants
{
    /// <summary>
    /// List of interpolated string as messages for exceptions.
    /// </summary>
    public static class ExceptionMessages
    {
        /// <summary>
        /// Collection is empty exception message.
        /// </summary>
        public const string EmptyCollection = "{0} contains no elements.";

        /// <summary>
        /// String isNullOrEmpty check message.
        /// </summary>
        public const string StringIsNullOrEmpty = "{0} cannot be null or empty.";

        /// <summary>
        /// <see cref="HourData" /> validation message for hours field.
        /// </summary>
        public const string TimeNotValidHours = "{0} has not valid hours.";

        /// <summary>
        /// <see cref="HourData" /> validation message for minutes field.
        /// </summary>
        public const string TimeNotValidMinutes = "{0} has not valid minutes";

        /// <summary>
        /// Missing resources expcetion message.
        /// </summary>
        public const string MissingResources = "Missing resource: {0} from: {1}";
    }
}