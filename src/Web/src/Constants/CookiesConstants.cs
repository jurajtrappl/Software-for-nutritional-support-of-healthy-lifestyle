namespace Application.Web.Constants
{
    /// <summary>
    /// Used cookies names and durations.
    /// </summary>
    internal static class CookiesConstants
    {
        /// <summary>
        /// Duration of chosen plan in advance cookie in hours.
        /// </summary>
        internal const int ChosenPlanInAdvanceDuration = 1;

        /// <summary>
        /// Name of the chosen plan in advance cookie.
        /// </summary>
        internal const string ChosenPlanInAdvanceName = nameof(ChosenPlanInAdvanceName);

        /// <summary>
        /// Duration of culture cookie.
        /// </summary>
        internal const int CultureDuration = 1;
    }
}